using Godot;
using System;

public partial class Player : Character
{

    [Export] private AttackComponent attackComponent;
    [Export] private PlayerBrain playerBrain;
    [Export] private InteractionComponent interactionComponent;

    public override void _Ready()
    {
        base._Ready();

        playerBrain = (PlayerBrain)brain;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        
        if (!Velocity.IsZeroApprox())
        {
            attackComponent.Rotation = brain.GetAttackDirection().Angle();

            interactionComponent.Rotation = brain.GetAttackDirection().Angle();
        }
    }

    protected override void Die()
    {
        GD.Print("GAME OVER");
    }

}
