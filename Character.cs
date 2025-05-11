using Godot;
using System;

public partial class Character : CharacterBody2D
{
    [Signal] public delegate void AttackingEventHandler(int attackType);

    public void Attack()
    {
        EmitSignal(nameof(Attacking), (int)AttackType.BaseMelee);
    }
}
