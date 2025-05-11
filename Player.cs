using Godot;
using System;

public partial class Player : Character
{

    private AttackComponent attackComponent;

    public override void _Ready()
    {
        base._Ready();
        attackComponent = (AttackComponent)FindChild("AttackComponent");

    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = new Vector2(0.0f, 0.0f);
        if (Input.IsActionPressed("up"))
        {
            direction.Y = -1.0f;
        }
        if (Input.IsActionPressed("down"))
        {
            direction.Y = 1.0f;
        }
        if (Input.IsActionPressed("left"))
        {
            direction.X = -1.0f;
        }
        if (Input.IsActionPressed("right"))
        {
            direction.X = 1.0f;
        }
        Velocity = direction.Normalized() * 200;
        MoveAndSlide();

        // Update attack component based on movement
        if (!direction.IsZeroApprox())
        {
            attackComponent.Rotation = direction.Angle();
        }
    }

}
