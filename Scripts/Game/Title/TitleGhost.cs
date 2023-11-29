using Godot;
using System;

public partial class TitleGhost : Node2D
{
    public Vector2 Target { get; set; }
    public Vector2 StartPosition { get; set; }
    public float Speed { get; set; } = 200f;

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (GlobalPosition.DistanceTo(Target) <= 2.2f * (Speed / 100f)) return;

        Vector2 movement = GlobalPosition.DirectionTo(Target);
        movement *= Speed;
        GlobalPosition += movement * (float)delta;
        GetNode<Sprite2D>("Sprites/EnemySprite").FlipH = GlobalPosition.X > Target.X;
    }

    public void GoHome()
    {
        Target = StartPosition;
    }
}
