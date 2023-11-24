using Godot;
using System;

public partial class BombItem : Item
{
    public HarryCharacter EffectedCharacter;
    public override void _Ready()
    {
        base._Ready();
        BodyEntered += OnCharacterEnterBody;
    }

    public override void ApplyItem()
    {
        EffectedCharacter.GetNode<ProjectileLauncherComponent>("Components/BombProjectileLauncher").CurrentAmmunition += 1;
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
