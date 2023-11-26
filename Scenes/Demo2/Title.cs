using Godot;
using Godot.Collections;
using System;

public partial class Title : Node2D
{
    [Export]
    public PackedScene Node;

    private Array<Node> _markers;
    private int spawnIndex = 0;

    public override void _Ready()
    {
        _markers = GetNode<Node2D>("Markers").GetChildren();   
    }

    public override void _Process(double delta)
    {
        if (spawnIndex >= _markers.Count) return;

        PathFollow2D path = GetNode<PathFollow2D>("Path2D/PathFollow2D");
        path.ProgressRatio = GD.Randf();
        TitleGhost titleGhost = (TitleGhost)Node.Instantiate();
        titleGhost.GlobalPosition = path.GlobalPosition;
        titleGhost.Marker = _markers[spawnIndex] as Marker2D;
        AddChild(titleGhost);
        spawnIndex++;

    }
}
