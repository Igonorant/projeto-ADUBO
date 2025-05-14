using Godot;
using System;
using System.Diagnostics;

public partial class Hurtbox : Area2D
{
    [Signal] public delegate void HurtboxHitEventHandler(EffectPackage effect);

    public void IsHitByLifeAlteringEffect(EffectPackage effect)
    {
        GD.Print("DOEU!");

        EmitSignal(nameof(HurtboxHit), effect);
    }
}

public partial class EffectPackage : Resource
{
    public int damage;
    public float stunTime;
    public float knockback;
    public float knockDuration;
    public Vector2 offenderPosition;

}
