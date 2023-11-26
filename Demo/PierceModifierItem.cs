using Godot;
using System;

public partial class PierceModifierItem : Item
{
    public Entity EffectedCharacter;
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
        if (character is Entity)
        {
            EffectedCharacter = (Entity)character;
            ApplyItem();
            QueueFree();
        }
    }
}
