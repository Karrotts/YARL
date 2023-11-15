using Godot;
using System;

public partial class Projectile : Area2D
{
    [Export(PropertyHint.Range, "0,10000")]
    public float Speed { get; set; }

    [Export(PropertyHint.Range, "0,10000")]
    public float Lifespan { get; set; }

    [Export(PropertyHint.Range, "0,10000")]
    public float Size { get; set; }

    [Export]
    public bool IsHoming { get; set; }

    [Export]
    public bool IsBoomerang { get; set; }

    [Export(PropertyHint.Range, "0,10000")]
    public int Damage { get; set; }

    [Export]
    public Vector2 MovingDirection { get; set; }

    [Export]
    public Vector2 StartingPosition { get; set; }

    [Export]
    public float Drag { get; set; }

    private float _currentSpeed;

    public override void _Ready()
    {
        Position = StartingPosition;
        Scale *= Size;
        _currentSpeed = Speed;

        // setup despawn timer
        Timer despawnTimer = GetNode<Timer>("DespawnTimer");
        despawnTimer.WaitTime = Lifespan;
        despawnTimer.Timeout += () => QueueFree();
        despawnTimer.Start();
    }

    public override void _Process(double delta)
    {
        HandleProjectileMovement(delta);
    }

    public virtual void HandleProjectileMovement(double delta)
    {
        Vector2 movement = MovingDirection;
        Rotation = MovingDirection.Angle();

        // apply basic forces
        movement *= _currentSpeed;

        // apply extensions (homing / boomerang) TODO

        Position += movement * (float)delta;
        MovingDirection = movement.Normalized();
        _currentSpeed -= Drag;
    }
}
