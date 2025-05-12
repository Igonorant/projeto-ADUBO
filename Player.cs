using Godot;
using System;

public partial class Player : Character
{

    [Export] private AttackComponent attackComponent;
    [Export] private PlayerBrain brain;
    [Export] private InteractionComponent interactionComponent;

    public override void _Ready()
    {
        base._Ready();

    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = brain.GetMovingDirection();
        Velocity = direction * 200;

        if (!Velocity.IsZeroApprox())
        {
            attackComponent.Rotation = brain.GetAttackDirection().Angle();

            interactionComponent.Rotation = brain.GetAttackDirection().Angle();
        }

        MoveAndSlide();
    }

    protected override void Die()
    {
        GD.Print("GAME OVER");
    }

}
