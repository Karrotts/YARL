using Godot;
using System;

public partial class HealthMaxItem : Item
{
    public Entity EffectedCharacter;
    public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnCharacterEnterBody;
    }

    public override void ApplyItem()
    {
        if (EffectedCharacter.HealthComponent.MaxHealth < 30)
        {
            EffectedCharacter.HealthComponent.MaxHealth += 1;
        }
        EffectedCharacter.HealthComponent.CurrentHealth += 1;
    }

    public void OnCharacterEnterBody(Node2D character)
    {
        if (character is Entity)
        {
            EffectedCharacter = (Entity)character;
            ApplyItem();
            QueueFree();
        }
    }
}
