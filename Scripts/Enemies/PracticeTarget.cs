using Godot;
using System;

public partial class PracticeTarget : Entity
{
    [Export]
    public PackedScene DamageNumber { get; set; }

    public override void _Ready()
    {
        base._Ready();
        HealthComponent.HealthZero += OnHealthZero;
        HealthComponent.OnDamageTaken += OnDamageTaken;
    }
    public void OnHealthZero()
    {
        QueueFree();
    }

    public void OnDamageTaken(int amount)
    {
        DamageText damageText = (DamageText)DamageNumber.Instantiate();
        damageText.Number = amount;
        damageText.Position = GetNode<Marker2D>("Marker2D").GlobalPosition;
        GetTree().Root.AddChild(damageText);
    } 
}
