using Godot;
using System;

public partial class PlayerBrain : BrainComponent
{

    private Vector2 last_direction = Vector2.Zero;

    public override void _Ready()
    {
        base._Ready();

    }

    public override void _PhysicsProcess(double delta)
    {
        if(!isStunned)
        {
            if (Input.IsActionJustPressed("attack"))
            {
                parentCharacter.Attack();
            }
        }

    }

    public override Vector2 GetMovingDirection()
    {
        if (!isStunned)
        {
            last_direction = Vector2.Zero;
            if (Input.IsActionPressed("up"))
            {
                last_direction.Y = -1.0f;
            }
            if (Input.IsActionPressed("down"))
            {
                last_direction.Y = 1.0f;
            }
            if (Input.IsActionPressed("left"))
            {
                last_direction.X = -1.0f;
            }
            if (Input.IsActionPressed("right"))
            {
                last_direction.X = 1.0f;
            }

            if (last_direction.IsZeroApprox())
            {
                return Vector2.Zero;
            }

            last_direction = last_direction.Normalized();
            return last_direction;
        }
        else
        {
            return Vector2.Zero;
        }
       
    }

    public override Vector2 GetAttackDirection() { return last_direction; }

}
