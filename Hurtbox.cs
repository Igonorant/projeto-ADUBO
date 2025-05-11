using Godot;
using System;

public partial class Hurtbox : Area2D
{
[Signal] public delegate void HurtboxHitEventHandler(int effect);

    public void IsHitByLifeAlteringEffect(int effect)
    {
        EmitSignal(nameof(HurtboxHitEventHandler), effect);
    }
}
