using Godot;
using System;

public partial class Baby : Character
{

    [Export] public Character mommy;
    [Export] public float maxDistanceToMommy = 200.0f;
    [Export] public float babySpeed = 50.0f;
    [Export] public float wanderingTime = 2.0f;
    [Export] public float idleTime = 1.0f;
    [Export] public float followingTime = 2.0f;


    private BabyBrain brain;

    public override void _Ready()
    {
        brain = (BabyBrain)FindChild("BabyBrain");
        brain.SetMommy(mommy, maxDistanceToMommy);
        brain.SetTimers(idleTime, wanderingTime, followingTime);
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = brain.GetMovingDirection() * babySpeed;
        brain.SelectAnimation();
        MoveAndSlide();
    }
}
