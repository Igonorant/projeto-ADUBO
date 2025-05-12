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
    private bool activableState = true;

    [Export] private float cooldown;

    [Export] private int attackPower;

    [Export] private float stunTime;

    [Export] private float knockback;

    [Export] private AttackType attackType;

    [Export] private float attackDuration;

    [Export] private CollisionShape2D shortRangeShape;

    [Export] private Timer strikeTimer;

    [Export] private Timer cooldownTimer;

    [Signal] public delegate void CooldownEndEventHandler();


    public override void _Ready()
    {
        GetParent<Character>().Connect(Character.SignalName.AttackOrdered, new Callable(this, nameof(Attack)));

        strikeTimer.Timeout += OnTimerTimeOut;

        cooldownTimer.Timeout += OnCooldownTimerTimeout;

    }

    private void Attack(int attackType)
    {
        if (activableState)
        {
            switch ((AttackType)attackType)
            {
                case AttackType.BaseMelee:

                    // Set the timer duration
                    strikeTimer.WaitTime = attackDuration;

                    strikeTimer.Start();

                    shortRangeShape.SetDeferred("disabled", false);

                    StartCooldown(cooldown);

                    break;
            }
        }
        else
        {
            GD.Print("COOLDOWN!");
        }
    }

    private void OnTimerTimeOut()
    {
        GD.Print("ATAQUEI!");
        shortRangeShape.SetDeferred("disabled", true);

        strikeTimer.Stop();
    }

    public void OnBodyEntered(Node2D body)
    {
        GD.Print("ACERTEI!");

        Hurtbox hurtbox = (Hurtbox)body;

        EffectPackage package = new EffectPackage();

        package.damage = -attackPower;

        package.stunTime = stunTime;

        package.knockback = knockback;

        package.offenderPosition = GetParent<Character>().GlobalPosition;

        hurtbox.IsHitByLifeAlteringEffect(package);

        shortRangeShape.SetDeferred("disabled", true);

    }

    private void StartCooldown(float waitTime)
    {
        cooldownTimer.WaitTime = waitTime;

        activableState = false;

        cooldownTimer.Start();
    }

    private void OnCooldownTimerTimeout()
    {
        activableState = true;

        EmitSignal(nameof(CooldownEnd));

        cooldownTimer.Stop();
    }
}
