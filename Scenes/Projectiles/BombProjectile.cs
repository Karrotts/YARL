using Godot;
using System;

public partial class BombProjectile : Projectile
{
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void OnBodyEntered(Node2D node)
    {
        if (node.GetGroups().Contains("Enemy"))
        {
            (node as Enemy).HealthComponent.DealDamage((int)(GD.RandRange(MinDamage, MaxDamage) * DamageModifier));
            (node as Enemy).MovementComponent.ApplyCounterForce(GlobalPosition.DirectionTo(node.GlobalPosition), 1000f);
        }
    }
}
