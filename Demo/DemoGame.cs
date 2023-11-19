using Godot;
using System;

public partial class DemoGame : Node2D
{
    [Export]
    public PackedScene[] Enemies { get; set; }

    public int Wave { get; set; } = 1;

    private bool _canSpawn = true;
    private Timer _spawnTimer;
    private Timer _waveTimer;
    private float _healthModifier = 0.2f;
    public override void _Ready()
    {
        base._Ready();
        _spawnTimer = GetNode<Timer>("SpawnTimer");
        _waveTimer = GetNode<Timer>("WaveTimer");
        _spawnTimer.Timeout += () => _canSpawn = true;
        _waveTimer.Timeout += SetUpNextWave;
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
        enemy.HealthComponent.CurrentHealth += (int)(enemy.HealthComponent.CurrentHealth * (_healthModifier * (Wave - 1)));
    }

    public void SetUpNextWave()
    {
        // enemies spawn 15% faster
        _spawnTimer.WaitTime *= 0.85f;
        Wave++;
    }
}
