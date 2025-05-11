using Godot;
using System;

public partial class Character : CharacterBody2D
{
    [Signal]
    public delegate void AttackingEventHandler(AttackType attackType);
}
