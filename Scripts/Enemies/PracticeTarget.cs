using Godot;
using System;

public partial class PracticeTarget : Enemy
{
    public override void _Ready()
    {
        base._Ready();
        HealthComponent.HealthZero += OnHealthZero;
    }
    public void OnHealthZero()
    {
        QueueFree();
    }
}
