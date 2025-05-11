using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

[GlobalClass]
public partial class HealthComponent : Node2D
{
    private int tempHealth;
    
    [Export] private int maxHealth;
   
    [Signal] public delegate void HealthSetEventHandler(int health);    
    
    [Signal] public delegate void HealthChangedEventHandler(int change);
    
    [Signal] public delegate void HealthDepletedEventHandler();
    
    public override void _Ready()
    {
        tempHealth = maxHealth;
        
        EmitSignal(nameof(HealthSetEventHandler), tempHealth);
        
    }
    
    private void ReceiveDamage(int dmg)
    {
        tempHealth -= dmg;
        
        if(tempHealth <= 0)
        {
            tempHealth = 0;
            Die();
        }
        else
        {
           EmitSignal(nameof(HealthChangedEventHandler), -dmg);         
        }
    }
    
    private void Heal(int heal)
    {
        tempHealth += heal;
        
        if(tempHealth > maxHealth)
        {
            tempHealth = maxHealth;
        }
          
        EmitSignal(nameof(HealthChangedEventHandler), heal);
    }
    
    private void Die()
    {
        EmitSignal(nameof(HealthDepletedEventHandler));
    }

}
