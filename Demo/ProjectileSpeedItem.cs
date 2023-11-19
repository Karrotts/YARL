using Godot;
using System;

public partial class ProjectileSpeedItem : Item
{
    public HarryCharacter EffectedCharacter;
    public override void _Ready()
    {
        BodyEntered += OnCharacterEnterBody;
    }

    public override void ApplyItem()
    {
        EffectedCharacter.ProjectileLauncherComponent.ProjectileCooldown *= 0.85f;
    }

    public void OnCharacterEnterBody(Node2D character)
    {
        if (character is HarryCharacter)
        {
            EffectedCharacter = (HarryCharacter)character;
            EffectedCharacter.CharacterInventory.AddItem(this);
            QueueFree();
        }
    }
}
