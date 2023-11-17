using Godot;

public partial class ProjectileLauncherComponent : Node2D
{
    [Export]
    public PackedScene Projectile { get; set; }

    [Export]
    public int ProjectileAmount { get; set; } = 1;

    [Export]
    public float ProjectileRotationAmount { get; set; } = 0f;

    [Export(PropertyHint.Range, "0,100")]
    public float ProjectileCooldown { get; set; } = 1f;

    [Export]
    public bool ProjectileToggle { get; set; } = true;

    [Export]
    public bool RotateTowardsMouse { get; set; } = false;

    [Export]
    [ExportGroup("Projectile Modifiers")]
    public float ProjectileSpeedModifier { get; set; } = 1f;

    [Export]
    [ExportGroup("Projectile Modifiers")]
    public float ProjectileSizeModifier { get; set; } = 1f;

    [Export]
    [ExportGroup("Projectile Modifiers")]
    public float ProjectileDamageModifier { get; set; } = 1f;

    [Export]
    [ExportGroup("Projectile Modifiers")]
    public float ProjectileDragModifier { get; set; } = 1f;

    [Export]
    [ExportGroup("Projectile Modifiers")]
    public bool ProjectileHomingModifier { get; set; } = false;

    [Export]
    [ExportGroup("Projectile Modifiers")]
    public bool ProjectileBoomerangModifier { get; set; } = false;


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
            SpawnProjectiles();
        }
    }

    private void SpawnProjectiles()
    {
        float rotationAmount = 0f + (-1 * ProjectileRotationAmount * (ProjectileAmount / 2));
        for (int i = 0; i < ProjectileAmount; i++)
        {
            Projectile projectile = (Projectile)Projectile.Instantiate();
            projectile.MovingDirection = (GetGlobalMousePosition() - GlobalPosition).Normalized().Rotated(rotationAmount);
            projectile.Rotate(projectile.MovingDirection.Angle());
            projectile.StartingPosition = GetNode<Marker2D>("ProjectileLaunchPoint").GlobalPosition;
            ApplyModifiersToProjectile(projectile);
            GetTree().Root.AddChild(projectile);
            rotationAmount += ProjectileRotationAmount;
        }
    }

    private void ApplyModifiersToProjectile(Projectile projectile)
    {
        // TODO: Maybe its better to have these as strength instead of booleans
        projectile.IsBoomerang = ProjectileBoomerangModifier;
        projectile.IsHoming = ProjectileHomingModifier;

        projectile.Speed *= ProjectileSpeedModifier;
        projectile.Size *= ProjectileSizeModifier;
        projectile.Drag *= ProjectileDragModifier;
        projectile.DamageModifier = ProjectileDamageModifier;
    }

    private void HandleRotation()
    {
        if (RotateTowardsMouse)
            LookAt(GetGlobalMousePosition());
    }

    private void StartCooldownTimer()
    {
        _projectileTimer.Start();
    }
}
