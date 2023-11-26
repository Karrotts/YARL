using Godot;
using System;

public partial class CharacterUI : CanvasLayer
{
    [Export]
    public Entity Character { get; set; }

    [Export]
    public DemoGame DemoGame { get; set; }

    [Export]
    public string Level { get; set; }

    public ProjectileLauncherComponent BombProjectileLauncherComponent { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Character.HealthComponent.Connect(HealthComponent.SignalName.HealthZero, Callable.From(PlayDeathAnimation));
        GetNode<Button>("RetryButton").Pressed += PlayRetryAnimation;
        BombProjectileLauncherComponent = Character.GetNode<ProjectileLauncherComponent>("Components/BombProjectileLauncher");
    }

    public override void _Process(double delta)
    {
        GetNode<Label>("HealthLabel").Text = "Health: " + Character.HealthComponent.CurrentHealth + "/" + Character.HealthComponent.MaxHealth;
        GetNode<Label>("WaveLabel").Text = "Wave: " + DemoGame.Wave;
        GetNode<Label>("BombLabel").Text = "Bombs: " + BombProjectileLauncherComponent.CurrentAmmunition + "/" + BombProjectileLauncherComponent.MaxAmmunition;
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

    public void StopGame()
    {
        DemoGame.StopGame();
    }

}
