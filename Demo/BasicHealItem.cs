using Godot;
using System;

public partial class BasicHealItem : Item
{
    public Character EffectedCharacter;
    public override void _Ready()
    {
        BodyEntered += OnCharacterEnterBody;
    }

    public override void ApplyItem()
    {
        EffectedCharacter.HealthComponent.CurrentHealth += 2;
    }

    public void OnCharacterEnterBody(Node2D character)
    {
        if (character is Character)
        {
            EffectedCharacter = (Character)character;
            EffectedCharacter.CharacterInventory.AddItem(this);
            QueueFree();
        }
    }
}
