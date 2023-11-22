using Godot;
using System;

public partial class CharacterUI : CanvasLayer
{
    [Export]
    public Character Character { get; set; }

    [Export]
    public DemoGame DemoGame { get; set; }

    [Export]
    public string Level { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Character.HealthComponent.Connect(HealthComponent.SignalName.HealthZero, Callable.From(PlayDeathAnimation));
        GetNode<Button>("RetryButton").Pressed += PlayRetryAnimation;
    }

    public override void _Process(double delta)
    {
        GetNode<Label>("HealthLabel").Text = "Health: " + Character.HealthComponent.CurrentHealth + "/" + Character.HealthComponent.MaxHealth;
        GetNode<Label>("WaveLabel").Text = "Wave: " + DemoGame.Wave;
    }

    public void PlayDeathAnimation()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("death_screen");
    }

    public void PlayRetryAnimation()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("retry_animation");
    }

    public void RestartLevel()
    {
        GetTree().ChangeSceneToFile(Level);
    }

}
