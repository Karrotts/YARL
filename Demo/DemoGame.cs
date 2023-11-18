using Godot;
using System;

public partial class DemoGame : Node2D
{
    [Export]
    public PackedScene[] Enemies { get; set; }

    private bool _canSpawn = true;
    private Timer _spawnTimer;
    public override void _Ready()
    {
        base._Ready();
        _spawnTimer = GetNode<Timer>("SpawnTimer");
        _spawnTimer.Timeout += () => _canSpawn = true;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (_canSpawn)
        {
            SpawnEnemy();
            _spawnTimer.Start();
            _canSpawn = false;
        }
    }

    public void SpawnEnemy()
    {
        Enemy enemy = (Enemy)Enemies[GD.RandRange(0, Enemies.Length - 1)].Instantiate();
        PathFollow2D path = GetNode<PathFollow2D>("CharacterCamera/SpawnPath/PathFollow2D");
        path.ProgressRatio = GD.Randf();
        enemy.Position = path.GlobalPosition;
        GetTree().Root.AddChild(enemy);
    }
}
