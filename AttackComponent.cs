using Godot;
using System;

public enum AttackType
{
    BaseMelee,
    Ranged
}

[GlobalClass]
public partial class AttackComponent : Node2D
{
    [Export] private int attackPower;
    
    [Export] private float stunTime;
    
    [Export] private float knockback;
    
    [Export] private AttackType attackType;

    [Export] private float attackDuration;

    [Export] private CollisionShape2D shortRangeShape;

    [Export] private Timer timer;


    public override void _Ready()
    {
        GetParent<Character>().Connect(Character.SignalName.Attacking, new Callable(this, nameof(Attack)));

        timer.Timeout += OnTimerTimeOut;

    }

    private void Attack(int attackType)
    {
        switch ((AttackType)attackType)
        {
            case AttackType.BaseMelee:

                // Set the timer duration
                timer.WaitTime = attackDuration;

                timer.Start();

        shortRangeShape.SetDeferred("disabled", false);

                break;
        }

    }

    private void OnTimerTimeOut()
    {
        GD.Print("ATAQUEI!");
        shortRangeShape.Disabled = true;
        
        timer.Stop();
    }

    public void OnBodyEntered(Node2D body)
    {
        GD.Print("ACERTEI!");
    
        Hurtbox hurtbox = (Hurtbox)body;
        
        EffectPackage package = new EffectPackage();
        
        package.damage = -attackPower;
        
        package.stunTime = stunTime;
        
        package.knockback = knockback;
        
        hurtbox.IsHitByLifeAlteringEffect(package);
        
        shortRangeShape.SetDeferred("disabled", true);

    }

}
