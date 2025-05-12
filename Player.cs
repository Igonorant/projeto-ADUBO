using Godot;
using System;

public partial class Player : Character
{

    [Export] private AttackComponent attackComponent;
    [Export] private BrainComponent brainComponent;
    [Export] private InteractionComponent interactionComponent;

    public override void _Ready()
    {
        base._Ready();

    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = brainComponent.GetMovingDirection();
        Velocity = direction * 200;

        if (!Velocity.IsZeroApprox())
        {
            attackComponent.Rotation = brainComponent.GetAttackDirection().Angle();
            
            interactionComponent.Rotation = brainComponent.GetAttackDirection().Angle();
        }

        MoveAndSlide();
    }
    
    protected override void Die()
    {
        GD.Print("GAME OVER");
    }

}
