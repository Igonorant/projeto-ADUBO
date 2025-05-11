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

    
    private void _Ready()
    {
        GetParent<Character>().Connect(Character.SignalName.Attacking, new Callable(this, nameof(Attack)));
    }
    
    private void Attack(AttackType attackType)
    {
        
    }

}
