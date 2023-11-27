using Godot;
using System;

public partial class TitleGhost : Node2D
{
    public Marker2D Marker { get; set; }
    public float Speed { get; set; } = 200f;

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (GlobalPosition.DistanceTo(Marker.GlobalPosition) <= 2.2f * (Speed / 100f)) return;

        Vector2 movement = GlobalPosition.DirectionTo(Marker.GlobalPosition);
        movement *= Speed;
        GlobalPosition += movement * (float)delta;
        GetNode<Sprite2D>("Sprites/EnemySprite").FlipH = GlobalPosition.X > Marker.GlobalPosition.X;
    }
}
