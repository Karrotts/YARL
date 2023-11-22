using Godot;
using System;

public partial class PierceModifierItem : Item
{
    public Character EffectedCharacter;
    public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnCharacterEnterBody;
    }

    public override void ApplyItem()
    {
        EffectedCharacter.GetNode<ProjectileLauncherComponent>("Components/ProjectileLauncherComponent").ProjectilePierceModifier += 1;
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