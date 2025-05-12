using Godot;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;

[GlobalClass]
public partial class BrainComponent : Node
{
    [Export] private Timer stunTimer;

    protected bool isStunned;

    [Export] protected Hurtbox hurtbox;

    protected Character parentCharacter;

    public override void _Ready()
    {
        parentCharacter = GetParent<Character>();

        hurtbox.Connect(Hurtbox.SignalName.HurtboxHit, new Callable(this, nameof(BeingHitHandler)));

        stunTimer.Timeout += EndStun;

    }

    public virtual Vector2 GetMovingDirection() { return Vector2.Zero; }

    public virtual Vector2 GetAttackDirection() { return Vector2.Zero; }

    public void BeingHitHandler(EffectPackage effect)
    {
        if (effect.stunTime > 0)
        {
            Stun(effect.stunTime);
        }

        ApplyKnockback(effect.offenderPosition, effect.knockback);
    }

    protected void Stun(float duration)
    {
        stunTimer.WaitTime = duration;
        stunTimer.Start();
        isStunned = true;
    }

    protected void EndStun()
    {
        isStunned = false;
        stunTimer.Stop();
    }

    private void ApplyKnockback(Vector2 offenderPosition, float knockbackIntensity)
    {
        Vector2 attackDirection = parentCharacter.GlobalPosition - offenderPosition;

        // Following line make the character move accordingly to its speed following knockback direction.
        // Since the intensity will depend on who is being hit, is not very relliable.
        // parentCharacter.knockbackDirection = attackDirection.Normalized() * knockbackIntensity;

        // An alternative is to make the intensity fully control the knockback movement, but this way it causes
        // a little "snapping" in the character.
        parentCharacter.Velocity = attackDirection.Normalized() * knockbackIntensity;
        parentCharacter.MoveAndSlide();

        // The ultimate alternative would need a bigger refactor to create the correct movement.
        // If we have time we may consider it.
    }

}
