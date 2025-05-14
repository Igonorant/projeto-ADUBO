using Godot;
using System;

public partial class Character : CharacterBody2D
{

    protected bool canAct;

    [Export] protected float speed;
    
    protected bool underKnockback;

    public Vector2 knockbackDirection = Vector2.Zero;

    [Export] protected BrainComponent brain;
    [Export] protected HealthComponent healthComponent;
    [Signal] public delegate void AttackOrderedEventHandler(int attackType);
    [Export] private Timer knockbackTimer;  
    [Export] protected Hurtbox hurtbox;


    public override void _Ready()
    {
        healthComponent.Connect(HealthComponent.SignalName.HealthDepleted, new Callable(this, nameof(Die)));
    
        hurtbox.Connect(Hurtbox.SignalName.HurtboxHit, new Callable(this, nameof(BeingHitHandler)));
    
        knockbackTimer.Timeout += EndKnockback;
    
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if(!underKnockback )
        {
        
            Velocity = brain.GetMovingDirection() * speed;

            brain.SelectAnimation();

            MoveAndSlide();
        
        }
        else
        {
            MoveAndSlide();
        }

    }

    public void Attack()
    {
        EmitSignal(nameof(AttackOrdered), (int)AttackType.BaseMelee);

    }

    protected virtual void Die()
    {
        //left empty
    }
    
    private void BeingHitHandler(EffectPackage effect)
    {
        if(effect.knockback > 0)
        {
            ApplyKnockback(effect.offenderPosition, effect.knockback, effect.knockDuration);     
        }

    } 
    
    
        private void ApplyKnockback(Vector2 offenderPosition, float intensity, float duration)
    {
        underKnockback = true;
        
        knockbackTimer.WaitTime = duration;
        knockbackTimer.Start();
    
        Vector2 attackDirection = this.GlobalPosition - offenderPosition;

        // Following line make the character move accordingly to its speed following knockback direction.
        // Since the intensity will depend on who is being hit, is not very relliable.
        // parentCharacter.knockbackDirection = attackDirection.Normalized() * knockbackIntensity;

        // An alternative is to make the intensity fully control the knockback movement, but this way it causes
        // a little "snapping" in the character.
        this.Velocity = attackDirection.Normalized() * intensity;
        
        this.MoveAndSlide();

        // The ultimate alternative would need a bigger refactor to create the correct movement.
        // If we have time we may consider it.
    }
    
    private void EndKnockback()
    {
        underKnockback = false;
        
        knockbackTimer.Stop();
    }
}
