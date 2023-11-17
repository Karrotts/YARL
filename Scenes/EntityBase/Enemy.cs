using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    [Export]
    public HealthComponent HealthComponent { get; set; }

    [Export]
    public MovementComponent MovementComponent { get; set; }
}
