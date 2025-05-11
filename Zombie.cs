using Godot;
using System;



public partial class Zombie : Character
{
    [Export] public CharacterBody2D mommy;
    [Export] public CharacterBody2D baby;
    [Export] public float speed = 50.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 distanceToMommy = mommy.GlobalPosition - GlobalPosition;
        if (distanceToMommy.Length() < 75.0f)
        {
            Velocity = distanceToMommy.Normalized() * speed;
        }
        else
        {
            Vector2 distanceToBaby = baby.GlobalPosition - GlobalPosition;
            Velocity = distanceToBaby.Normalized() * speed;
        }
        MoveAndSlide();
    }
}
