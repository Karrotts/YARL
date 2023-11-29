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
        GetNode<Button>("ExitButton").Pressed += ExitGame;
        GetNode<Button>("StartButton").Pressed += StartGame;
    }

    public override void _Process(double delta)
    {
        SpawnGhosts();
    }

    private void SpawnGhosts()
    {
        if (spawnIndex >= _markers.Count) return;
        PathFollow2D path = GetNode<PathFollow2D>("Path2D/PathFollow2D");
        path.ProgressRatio = GD.Randf();
        TitleGhost titleGhost = (TitleGhost)Node.Instantiate();
        titleGhost.GlobalPosition = path.GlobalPosition;
        titleGhost.Target = (_markers[spawnIndex] as Marker2D).GlobalPosition;
        titleGhost.StartPosition = titleGhost.GlobalPosition;
        AddChild(titleGhost);
        spawnIndex++;
    }

    private async void StartGame()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("title_exit");
        Array<Node> nodes = GetChildren();
        foreach (Node node in nodes)
        {
            if (node is TitleGhost)
            {
                (node as TitleGhost).GoHome();
            }
        }
        await ToSignal(GetTree().CreateTimer(2f), SceneTreeTimer.SignalName.Timeout);
        GetNode<GlobalControls>("/root/GlobalControls").GotoScene(SceneRepository.DemoLevel);
    }

    private void ExitGame()
    {
        GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
        GetTree().Quit();
    }
}
