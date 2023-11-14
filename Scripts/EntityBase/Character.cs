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

    [Export]
    [ExportGroup("Modifiers")]
    public int SpeedModifier
    {
        get { return _speedModifier; }
        set 
        {
            if (MovementComponent != null)
            {
                MovementComponent.CurrentSpeed += value - _speedModifier;
            }
            _speedModifier = value;
        }
    }

    private int _speedModifier;

    public override void _Ready()
    {
        // reset values to trigger update methods
        SpeedModifier = _speedModifier * -1;
        SpeedModifier = Mathf.Abs(_speedModifier);
    }

    public override void _Process(double delta)
    {
    }
}
