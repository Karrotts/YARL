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
        ProjectileLauncherComponent projectileLauncherComponent = EffectedCharacter.GetNode<ProjectileLauncherComponent>("Components/ProjectileLauncherComponent");
        if (projectileLauncherComponent.ProjectilePierceModifier < 5) {
            projectileLauncherComponent.ProjectilePierceModifier += 1;
        }

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
