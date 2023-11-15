using Godot;
using System;

public partial class Character : CharacterBody2D
{
    [Export]
    [ExportGroup("Nodes")]
    public HealthComponent HealthComponent { get; set; }

    [Export]
    [ExportGroup("Nodes")]
    public MovementComponent MovementComponent { get; set; }

    public CharacterInventory CharacterInventory { get; set; }

    public override void _Ready()
    {
        CharacterInventory = GetNode<CharacterInventory>("/root/CharacterInventory");
        CharacterInventory.ActiveCharacter = this;
    }

    public override void _Process(double delta)
    {
    }
}
