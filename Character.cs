using Godot;
using System;

public partial class Character : CharacterBody2D
{

    protected bool canAct;
    
    [Export] protected HealthComponent healthComponent;
    [Signal] public delegate void AttackOrderedEventHandler(int attackType);

    public override void _Ready()
    {
       healthComponent.Connect(HealthComponent.SignalName.HealthDepleted, new Callable(this, nameof(Die)));
    }

    public void Attack()
    {
        EmitSignal(nameof(AttackOrdered), (int)AttackType.BaseMelee);     

    }
    
    protected virtual void Die()
    {
        //left empty
    }
}
