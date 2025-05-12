using Godot;
using System;

public partial class CharacterSprite : Sprite2D
{
        [Export] private HealthComponent healthComponent;
        
        [Export] private Timer hurtIndicationTimer;
    
        [Export] private float hurtIndicationDuration;
         
        [Export] private Material hurtingMaterial;
        
        protected Material originalMaterial;
        
        protected bool isHurt = false;
        
        protected bool isInCooldown = false;
        
        
        public override void _Ready()
        {
        
        healthComponent.Connect(HealthComponent.SignalName.HealthChanged, new Callable(this, nameof(InitiateHurtIndication)));
        
        hurtIndicationTimer.Timeout += EndHurtIndication;
        
        originalMaterial = this.Material;
               
        }

        private void InitiateHurtIndication(int damage)
    {
        if(damage < 0)
        {
        
            isHurt = true;
            

            this.Material = hurtingMaterial;
                //this.SelfModulate = new Color(1, 0, 0, 1);            
            
  
        hurtIndicationTimer.WaitTime = hurtIndicationDuration;
        
        hurtIndicationTimer.Start();  
        }
    
    }
    
    private void EndHurtIndication()
    {   
        isHurt = false;
        
        this.Material = originalMaterial;
        //this.SelfModulate = new Color(0, 0, 0, 0);
        
        hurtIndicationTimer.Stop();
    }
}
