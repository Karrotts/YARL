using Godot;
using System;

public partial class CharacterUI : CanvasLayer
{
    [Export]
    public Character Character { get; set; }

    [Export]
    public DemoGame DemoGame { get; set; }

    public override void _Process(double delta)
    {
        GetNode<Label>("HealthLabel").Text = "Health: " + Character.HealthComponent.CurrentHealth + "/" + Character.HealthComponent.MaxHealth;
        GetNode<Label>("WaveLabel").Text = "Wave: " + DemoGame.Wave;
    }

}
