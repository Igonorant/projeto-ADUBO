using Godot;
using System;

public partial class Character : CharacterBody2D
{
    [Export] protected HealthComponent healthComponent;
    [Signal] public delegate void AttackingEventHandler(int attackType);

    public override void _Ready()
    {
       healthComponent.Connect(HealthComponent.SignalName.HealthDepleted, new Callable(this, nameof(Die)));
    }

    public void Attack()
    {
        EmitSignal(nameof(Attacking), (int)AttackType.BaseMelee);     

    }
    
    protected virtual void Die()
    {
        //left empty
    }
}
