using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class InteractionComponent : Node2D
{
    [Export] private CollisionShape2D interactionArea;

    [Export] private PlayerBrain playerBrain;
    
    [Export] Timer interactionTimer;
    
    [Export] float interactionDelay;

    public override void _Ready()
    {
        playerBrain.Connect(PlayerBrain.SignalName.InteractionOrdered, new Callable(this, nameof(Interact)));

        interactionTimer.Timeout += DisableInteractionArea;

    }

    private void Interact()
    {
        interactionTimer.WaitTime = interactionDelay;
        interactionTimer.Start();
    }

    private void DisableInteractionArea()
    {
        interactionArea.SetDeferred("disabled", true);
    }
    
    public void OnBodyDetected(Node2D body)
    {
        GD.Print("InTERACT!!");
    }
}

