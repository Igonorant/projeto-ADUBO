using Godot;
using System;

public partial class CharacterSprite : Sprite2D
{
        [Export] private HealthComponent healthComponent;
        
        [Export] private Timer hurtIndicationTimer;
    
        [Export] private float hurtIndicationDuration;
        
        public override void _Ready()
        {
        
        healthComponent.Connect(HealthComponent.SignalName.HealthChanged, new Callable(this, nameof(InitiateHurtIndication)));
        
        hurtIndicationTimer.Timeout += EndHurtIndication;
        
        }

        private void InitiateHurtIndication(int damage)
    {
        if(damage < 0)
        {
                  this.SelfModulate = new Color(1, 0, 0, 1);
    
        hurtIndicationTimer.WaitTime = hurtIndicationDuration;
        
        hurtIndicationTimer.Start();  
        }
    
    }
    
    private void EndHurtIndication()
    {   
        this.SelfModulate = new Color(0, 0, 0, 0);
        
        hurtIndicationTimer.Stop();
    }
}
