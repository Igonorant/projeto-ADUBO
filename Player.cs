using Godot;
using System;

public partial class Player : Character
{
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
    }

}
