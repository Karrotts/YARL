using Godot;
using System;

public partial class StatModifierComponent : Node
{
    [Export]
    [ExportGroup("Components")]
    public HealthComponent HealthComponent { get; set; }

    [Export]
    [ExportGroup("Components")]
    public MovementComponent MovementComponent { get; set; }

    [Export]
    [ExportGroup("Components")]
    public ProjectileLauncherComponent ProjectileLauncherComponent { get; set; }

    [Export]
    [ExportGroup("Modifiers")]
    [ExportSubgroup("Health")]
    public int MaxHealth { get; set; }

    [Export]
    [ExportGroup("Modifiers")]
    [ExportSubgroup("Movement")]
    public float MovementSpeed;

    [Export]
    [ExportGroup("Modifiers")]
    [ExportSubgroup("Projectile Launcher")]
    public int ProjectileAmount;

    [Export]
    [ExportGroup("Modifiers")]
    [ExportSubgroup("Projectile Launcher")]
    public float ProjectileCooldown;

    [Export]
    [ExportGroup("Modifiers")]
    [ExportSubgroup("Projectiles")]
    public float ProjectileSpeed;

    [Export]
    [ExportGroup("Modifiers")]
    [ExportSubgroup("Projectiles")]
    public float ProjectileSize;

    [Export]
    [ExportGroup("Modifiers")]
    [ExportSubgroup("Projectiles")]
    public float ProjectileDrag;


    /// <summary>
    /// Applys the modifiers to a projectile
    /// </summary>
    /// <param name="projectile"></param>
    public void ApplyProjectileModifiers(Projectile projectile)
    {

    }

}
