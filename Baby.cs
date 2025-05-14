using Godot;
using System;

public partial class Baby : Character
{

    [Export] private Character mommy;
    [Export] private float maxDistanceToMommy = 200.0f;
    [Export] private float wanderingTime = 2.0f;
    [Export] private float idleTime = 1.0f;
    [Export] private float followingTime = 2.0f;
    [Export] private BabyBrain babyBrain;

    public override void _Ready()
    {
        base._Ready();
    
        babyBrain = (BabyBrain)brain;
        
        babyBrain.SetMommy(mommy, maxDistanceToMommy);
        babyBrain.SetTimers(idleTime, wanderingTime, followingTime);
    }

}
