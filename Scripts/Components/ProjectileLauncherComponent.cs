using Godot;

public partial class ProjectileLauncherComponent : Node2D
{
    [Export]
    public PackedScene Projectile { get; set; }

    [Export(PropertyHint.Range, "0,100")]
    public float ProjectileCooldown { get; set; } = 1f;

    [Export]
    public bool ProjectileToggle { get; set; } = true;

    public bool ProjectileOnCooldown
    {
        get { return _projectileOnCooldown; }
        set 
        { 
            if (value != _projectileOnCooldown)
            {
                _projectileOnCooldown = value;
                if (value) StartCooldownTimer();
            }
        }
    }

    public bool CanProjectile { 
        get { return ProjectileToggle && !ProjectileOnCooldown; } 
    }

    private Timer _projectileTimer;
    private bool _projectileOnCooldown;

    public override void _Ready()
    {
        _projectileTimer = GetNode<Timer>("ProjectileCooldownTimer");
        _projectileTimer.WaitTime = ProjectileCooldown;
        _projectileTimer.Timeout += () => ProjectileOnCooldown = false;
        ProjectileOnCooldown = false;
    }

    public override void _Process(double delta)
    {
        HandleAction();
        HandleRotation();
    }

    private void HandleAction()
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left) && CanProjectile)
        {
            ProjectileOnCooldown = true;
            Projectile projectile = (Projectile)Projectile.Instantiate();
            projectile.MovingDirection = (GetGlobalMousePosition() - GlobalPosition).Normalized();
            projectile.StartingPosition = GetNode<Marker2D>("ProjectileLaunchPoint").GlobalPosition;
            projectile.Rotation = projectile.MovingDirection.Angle();
            GetTree().Root.AddChild(projectile);
        }
    }

    private void HandleRotation()
    {
        LookAt(GetGlobalMousePosition());
    }

    private void StartCooldownTimer()
    {
        _projectileTimer.Start();
    }
}
