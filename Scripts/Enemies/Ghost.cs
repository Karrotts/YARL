using Godot;
using System;

public partial class Ghost : Entity
{
    public Entity Target { get; set; }
    public PathFollow2D Path { get; set; }

    public PackedScene DamageNumber { get; set; }

    private AnimationPlayer _generalAnimationPlayer;
    private AnimationPlayer _hitAnimationPlayer;
    private bool _playerInAttackRange = false;

    public override void _Ready()
    {
        base._Ready();

        DamageNumber = ResourceLoader.Load<PackedScene>(SceneRepository.DamageNumber);

        HealthComponent.HealthZero += OnHealthZero;
        HealthComponent.OnDamageTaken += OnDamageTaken;

        Area2D attackRangeArea = GetNode<Area2D>("AttackRange");
        attackRangeArea.BodyEntered += OnCharacterEnterAttackRange;
        attackRangeArea.BodyExited += OnCharacterExitAttackRange;

        _generalAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayers/GeneralAnimation");
        _hitAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayers/HitAnimation");

        // set the player as the target
        foreach(Node node in GetTree().Root.GetNode<Node2D>("DemoLevel").GetChildren())
        {
            if (node.GetGroups().Contains("Character"))
            {
                Target = node as Entity;
                break;
            }
        }
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
        damageText.Position = GetNode<Marker2D>("HitSpawnPoint").GlobalPosition;
        GetTree().Root.AddChild(damageText);
        _hitAnimationPlayer.Play("hit");
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
            _playerInAttackRange = true;
        }
    }

    private void OnCharacterExitAttackRange(Node2D node)
    {
        if (node.GetGroups().Contains("Character"))
        {
            _playerInAttackRange = false;
        }
    }
}
