using Godot;
using System;

public partial class ArrowItem : Item
{
    public Character EffectedCharacter;
    public override void _Ready()
    {
        BodyEntered += OnCharacterEnterBody;
    }

    public override void ApplyItem()
    {
        EffectedCharacter.GetNode<ProjectileLauncherComponent>("Components/ProjectileLauncherComponent").ProjectileAmount += 1;
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
