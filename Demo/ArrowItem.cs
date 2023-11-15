using Godot;
using System;

public partial class ArrowItem : Area2D, IItem
{
    public Character EffectedCharacter;
    public override void _Ready()
    {
        BodyEntered += OnCharacterEnterBody;
    }

    public void ApplyItem()
    {
        EffectedCharacter.GetNode<ProjectileLauncherComponent>("Components/ProjectileLauncherComponent").ProjectileAmount += 1;
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
