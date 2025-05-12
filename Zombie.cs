using Godot;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;



public partial class Zombie : Character
{
    [Export] public Character mommy;
    [Export] public Character baby;
    [Export] public float speed = 50.0f;
    [Export] public float chasingMommyDistance = 75.0f;
    [Export] public float attackRange = 50.0f;


    private ZombieBrain brain;

    public override void _Ready()
    {
        base._Ready();
        // attackComponent = (AttackComponent)FindChild("AttackComponent");
        brain = (ZombieBrain)FindChild("ZombieBrain");
        brain.SetTarget(baby);
        brain.SetAttackRange(attackRange);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 distanceToMommy = mommy.GlobalPosition - GlobalPosition;
        brain.SetTarget(distanceToMommy.Length() < chasingMommyDistance ? mommy : baby);

        Vector2 direction = brain.GetMovingDirection();
        Velocity = direction * speed;

        // if (!Velocity.IsZeroApprox())
        // {
        //     attackComponent.Rotation = brain.GetAttackDirection().Angle();
        // }

        MoveAndSlide();
    }


        protected override void Die()
    {
        GD.Print("Zombidead");
        
        this.QueueFree();
    }
}
