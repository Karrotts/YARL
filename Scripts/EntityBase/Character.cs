using Godot;
using System;

public partial class Character : CharacterBody2D
{
    [Export]
    public HealthComponent HealthComponent { get; set; }

    [Export]
    public MovementComponent MovementComponent { get; set; }

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
    {
    }
}
