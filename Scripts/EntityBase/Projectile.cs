using Godot;
using System;

public partial class Projectile : Area2D
{
    #region EXPORTS
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
    public int MinDamage { get; set; }

    [Export(PropertyHint.Range, "0,10000")]
    public int MaxDamage { get; set; }

    [Export(PropertyHint.Range, "0,10000")]
    public float DamageModifier { get; set; }

    [Export]
    public Vector2 MovingDirection { get; set; }

    [Export]
    public Vector2 StartingPosition { get; set; }

    [Export]
    public float Drag { get; set; }

    [Export]
    public int PierceCount { get; set; }

    [Export]
    public bool MoveOnRotation { get; set; }

    [Export]
    public string HitBody { get; set; } = "Enemy";
    #endregion

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

        BodyEntered += OnBodyEntered;
    }

    public override void _Process(double delta)
    {
        HandleProjectileMovement(delta);
    }

    public virtual void HandleProjectileMovement(double delta)
    {
        Vector2 movement = Position;

        // move towards rotation or direction
        if (MoveOnRotation)
        {
            movement.X = Mathf.Cos(Rotation);
            movement.Y = Mathf.Sin(Rotation);
        } 
        else
        {
            movement = MovingDirection;
        }

        Position += movement * _currentSpeed * (float)delta;
        _currentSpeed -= Drag * (float)delta;
    }

    public virtual void OnBodyEntered(Node2D node)
    {
        if (node.GetGroups().Contains(HitBody))
        {
            (node as Entity).HealthComponent.DealDamage((int)(GD.RandRange(MinDamage, MaxDamage) * DamageModifier));
            if (PierceCount <= 0)
            {
                QueueFree();
            } else
            {
                PierceCount--;
            }
        }
    }
}
