using Godot;
using System;

    public enum AttackType
    {
        BaseMelee,
        Ranged
    }

[GlobalClass]
public partial class AttackComponent : Node2D
{
    [Export] private int attackPower;
    [Export] private AttackType attackType;
    
    [Export] private float attackDuration;
    
    [Export] private CollisionShape2D shortRangeArea;
    
    [Export] private Timer timer;

    
    private void _Ready()
    {
        GetParent<Character>().Connect(Character.SignalName.Attacking, new Callable(this, nameof(Attack)));

        timer.Timeout += OnTimerTimeOut;
    
    }
    
    private void Attack(AttackType attackType)
    {
    switch(attackType)
    {
        case AttackType.BaseMelee:
        
        // Set the timer duration
        timer.WaitTime = attackDuration;

        timer.Start();
        
        shortRangeArea.Disabled = false;
        
        break;
    }

    }
    
    private void OnTimerTimeOut()
    {
        shortRangeArea.Disabled = true;
    }

}
