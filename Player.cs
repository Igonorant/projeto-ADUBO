using Godot;
using System;

public partial class Player : Character
{

    private AttackComponent attackComponent;
    private BrainComponent brainComponent;

    public override void _Ready()
    {
        base._Ready();
        attackComponent = (AttackComponent)FindChild("AttackComponent");
        brainComponent = (BrainComponent)FindChild("PlayerBrain");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = brainComponent.GetMovingDirection();
        Velocity = direction * 200;

        if (!Velocity.IsZeroApprox())
        {
            attackComponent.Rotation = brainComponent.GetAttackDirection().Angle();
        }

        MoveAndSlide();
    }

}
