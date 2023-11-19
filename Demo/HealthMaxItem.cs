using Godot;
using System;

public partial class HealthMaxItem : Item
{
    public Character EffectedCharacter;
    public override void _Ready()
    {
        BodyEntered += OnCharacterEnterBody;
    }

    public override void ApplyItem()
    {
        EffectedCharacter.HealthComponent.MaxHealth += 1;
        EffectedCharacter.HealthComponent.CurrentHealth += 1;
    }

    public void OnCharacterEnterBody(Node2D character)
    {
        if (character is Character)
        {
            EffectedCharacter = (Character)character;
            ApplyItem();
            QueueFree();
        }
    }
}
