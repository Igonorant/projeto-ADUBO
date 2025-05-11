using Godot;
using System;



public partial class Zombie : CharacterBody2D
{
    [Export] public CharacterBody2D target;
    [Export] public float speed = 50.0f;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = target.GlobalPosition - GlobalPosition;
        Velocity = direction.Normalized() * speed;
        MoveAndSlide();
    }
}
