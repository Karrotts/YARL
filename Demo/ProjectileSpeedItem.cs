using Godot;
using System;

public partial class ProjectileSpeedItem : Item
{
    public HarryCharacter EffectedCharacter;
    public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnCharacterEnterBody;
    }

    public override void ApplyItem()
    {
        if (EffectedCharacter.ProjectileLauncherComponent.ProjectileCooldown >= 0.1f)
        {
            EffectedCharacter.ProjectileLauncherComponent.ProjectileCooldown *= 0.95f;
        }
    }

    public void OnCharacterEnterBody(Node2D character)
    {
        if (character is HarryCharacter)
        {
            EffectedCharacter = (HarryCharacter)character;
            ApplyItem();
            QueueFree();
        }
    }
}
