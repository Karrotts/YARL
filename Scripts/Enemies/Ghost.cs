using Godot;
using System;

public partial class Ghost : Entity
{
    [Export]
    public PackedScene DamageNumber { get; set; }

    public Entity Target { get; set; }
    public PathFollow2D Path { get; set; }

    private AnimationPlayer _animationPlayer;
    private bool _ghostIsAttacking = false;
    private bool _playerInAttackRange = false;

    public override void _Ready()
    {
        base._Ready();
        foreach (Node node in GetTree().GetNodesInGroup("Character"))
        {
                Target = (node as Entity);
                break;
        }
        HealthComponent.HealthZero += OnHealthZero;
        HealthComponent.OnDamageTaken += OnDamageTaken;
        GetNode<Area2D>("AttackRange").BodyEntered += OnCharacterEnterAttackRange;
        GetNode<Area2D>("AttackRange").BodyExited += OnCharacterExitAttackRange;
        GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D").ScreenExited += RespawnEnemy;
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _Process(double delta)
    {
        HandleSpriteFlip();
        HandleGhostMovement(delta);

        if (!_ghostIsAttacking && !HealthComponent.IsInvulnerable) _animationPlayer.Play("be_ghostly");
        if (_ghostIsAttacking) _animationPlayer?.Play("attack");
    }

    public void OnHealthZero()
    {
        GetNode<EnemyDrops>("EnemyDrops").DropLoot();
        QueueFree();
    }

    public void OnDamageTaken(int amount)
    {
        DamageText damageText = (DamageText)DamageNumber.Instantiate();
        damageText.Number = amount;
        damageText.Position = GetNode<Marker2D>("Marker2D").GlobalPosition;
        GetTree().Root.AddChild(damageText);
        _animationPlayer.Play("hit");
    }

    public void HitPlayer()
    {
        if (_playerInAttackRange)
        {
            Target.HealthComponent.DealDamage(3);
        }
    }

    private void HandleSpriteFlip()
    {
        GetNode<Sprite2D>("Sprites/EnemySprite").FlipH = GlobalPosition.X > Target.GlobalPosition.X;
    }

    private void HandleGhostMovement(double delta)
    {
        MovementComponent.Move(GlobalPosition.DirectionTo(Target.GlobalPosition));
    }

    private void OnCharacterEnterAttackRange(Node2D node)
    {
        if (node.GetGroups().Contains("Character"))
        {
            _ghostIsAttacking = true;
            _playerInAttackRange = true;
        }
    }

    private void OnCharacterExitAttackRange(Node2D node)
    {
        if (node.GetGroups().Contains("Character"))
        {
            _ghostIsAttacking = false;
            _playerInAttackRange = false;
        }
    }

    private void RespawnEnemy()
    {
        Path.ProgressRatio = GD.Randf();
        GlobalPosition = Path.GlobalPosition;
    }
}
