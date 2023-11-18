using Godot;
using System;

public partial class Ghost : Enemy
{
    [Export]
    public PackedScene DamageNumber { get; set; }

    public Character Target { get; set; }

    public override void _Ready()
    {
        base._Ready();
        foreach (Node node in GetTree().GetNodesInGroup("Character"))
        {
                Target = (node as Character);
                break;
        }
        HealthComponent.HealthZero += OnHealthZero;
        HealthComponent.OnDamageTaken += OnDamageTaken;
    }

    public override void _Process(double delta)
    {
        HandleSpriteFlip();
        HandleGhostMovement(delta);
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

    private void HandleSpriteFlip()
    {
        GetNode<Sprite2D>("Sprites/EnemySprite").FlipH = GlobalPosition.X > Target.GlobalPosition.X;
    }

    private void HandleGhostMovement(double delta)
    {
        MovementComponent.Move(GlobalPosition.DirectionTo(Target.GlobalPosition));
    }
}
