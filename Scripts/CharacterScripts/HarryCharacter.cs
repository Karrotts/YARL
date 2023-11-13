using Godot;
using System;

public partial class HarryCharacter : Character
{
    [Export]
    public ProjectileLauncherComponent ProjectileLauncherComponent { get; set; }

    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        base._Ready();
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }
    public override void _Process(double delta)
    {
        base._Process(delta);
        HandlePlayerAnimation();
    }

    public void HandlePlayerAnimation()
    {
        // handle playing movement animation
        /**
        if (MovementComponent.IsMoving)
            _animationPlayer.Play("player_move");
        else
            _animationPlayer.Play("player_stop");
        */

        // handle flipping the player sprite and projectile launcher
        Sprite2D sprite = GetNode<Sprite2D>("CharacterSprite"); 
        bool flipValue = sprite.FlipH;
        sprite.FlipH = GetGlobalMousePosition().X < GlobalPosition.X;
        if (flipValue != sprite.FlipH)
            ProjectileLauncherComponent.Position = new Vector2(
                ProjectileLauncherComponent.Position.X * -1,
                ProjectileLauncherComponent.Position.Y
            );
    }
}
