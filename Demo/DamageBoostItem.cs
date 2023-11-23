using Godot;
using System;

public partial class DamageBoostItem : Item
{
    public HarryCharacter EffectedCharacter;
    public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnCharacterEnterBody;
    }

    public override void ApplyItem()
    {
        EffectedCharacter.ProjectileLauncherComponent.ProjectileDamageModifier += .25f;
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
