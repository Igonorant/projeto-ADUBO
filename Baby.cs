using Godot;
using System;

public partial class Baby : CharacterBody2D
{
    [Export] public CharacterBody2D mommy;
    [Export] public float maxDistanceToMommy = 500;
    [Export] public float babySpeed = 50;

    private float maxDistanceToMommySquared;
    private RandomNumberGenerator rng;

    private Vector2 currentDirection;

    public override void _Ready()
    {
        maxDistanceToMommySquared = maxDistanceToMommy * maxDistanceToMommy;
        rng = new RandomNumberGenerator();
        rng.Randomize();
        currentDirection = Vector2.Zero;
    }


    public override void _PhysicsProcess(double delta)
    {
        Vector2 distance_to_mommy = mommy.GlobalPosition - GlobalPosition;
        if (distance_to_mommy.LengthSquared() > maxDistanceToMommySquared)
        {
            currentDirection = Vector2.Zero;
            MoveAndCollide(distance_to_mommy.Normalized() * babySpeed * (float)delta);
        }
        else
        {
            if (currentDirection.IsZeroApprox())
            {
                currentDirection.X = rng.RandfRange(-1.0f, 1.0f);
                currentDirection.Y = rng.RandfRange(-1.0f, 1.0f);
                currentDirection = currentDirection.Normalized();
            }
            MoveAndCollide(currentDirection * babySpeed * (float)delta);
        }
    }
}
