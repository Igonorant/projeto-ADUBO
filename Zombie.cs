using Godot;
using System;



public partial class Zombie : CharacterBody2D
{
    [Export] public CharacterBody2D target;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = target.GlobalPosition - GlobalPosition;
        MoveAndCollide(direction.Normalized() * 150 * (float)delta);
    }
}
