using Godot;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;

public partial class Zombie : Character
{
    [Export] public Character mommy;
    [Export] public Character baby;
    [Export] public float chasingMommyDistance = 75.0f;
    [Export] public float attackRange = 50.0f;
    
    private ZombieBrain zombieBrain;


    public override void _Ready()
    {
        base._Ready();

        zombieBrain = (ZombieBrain)brain;

        zombieBrain.SetTarget(baby);
        zombieBrain.SetAttackRange(attackRange);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 distanceToMommy = mommy.GlobalPosition - GlobalPosition;
        zombieBrain.SetTarget(distanceToMommy.Length() < chasingMommyDistance ? mommy : baby);

        base._PhysicsProcess(delta);
    }


    protected override void Die()
    {
        GD.Print("Zombidead");

        this.QueueFree();
    }
}
