using Godot;

public partial class Upgrade : Resource
{
    [Export]
    public UpgradeOptions UpgradeOptions { get; set; }

    [Export]
    public float Modifier { get; set; }

    [Export]
    public Texture Texture { get; set; }

    [Export]
    public string UpgradeText { get; set; }

    public Upgrade() : this(0, 0f, null, "") { }

    public Upgrade(UpgradeOptions upgradeOptions, float modifier, Texture texture, string upgradeText)
    {
        UpgradeOptions = upgradeOptions;
        Modifier = modifier;
        Texture = texture;
        UpgradeText = upgradeText;
    }   
}

public enum UpgradeOptions
{
    HealthStatic,
    HealthPercentage,
    MovementSpeedStatic,
    MovementSpeedPercentage,
    ProjectileCount,
    ProjectileDamageStatic,
    ProjectileDamagePercentage,
    ProjectileCooldownStatic,
    ProjectileCooldownPercentage,
    BombStatic,
    BombPercentage,
}
