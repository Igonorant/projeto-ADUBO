using Godot;
using System;
using System.Diagnostics;

public partial class Hurtbox : Area2D
{
[Signal] public delegate void HurtboxHitEventHandler(int effect);

    public void IsHitByLifeAlteringEffect(int effect)
    {
        GD.Print("DOEU!");
    
        EmitSignal(nameof(HurtboxHit), effect);
    }
}
