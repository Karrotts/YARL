using Godot;
using System;

public partial class BasicHealItem : Item
{
    public Entity EffectedCharacter;
    public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnCharacterEnterBody;
    }

    public override void ApplyItem()
    {
        EffectedCharacter.HealthComponent.CurrentHealth += 2;
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
