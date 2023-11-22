using Godot;
using System;

public partial class EnemyDrops : Node2D
{
    [Export]
    public PackedScene[] LegendaryDrops { get; set; }

    [Export]
    public PackedScene[] RareDrops { get; set; }

    [Export]
    public PackedScene[] CommonDrops { get; set; }

    [Export]
    public float LegendaryDropChance { get; set; } = 0.1f;

    [Export]
    public float RareDropChance { get; set; } = 0.25f;

    [Export]
    public float CommonDropChance { get; set; } = 0.5f;
    
    public void DropLoot()
    {
        if (GD.Randf() <= CommonDropChance)
        {
            if (GD.Randf() <= RareDropChance)
            {
                if (GD.Randf() <= LegendaryDropChance)
                {
                    SpawnRandomItem(LegendaryDrops);
                }
                else
                {
                    SpawnRandomItem(RareDrops);
                }
            }
            else
            {
                SpawnRandomItem(CommonDrops);
            }
        }
    }

    public void SpawnRandomItem(PackedScene[] lootTable)
    {
        if (lootTable.Length == 0) return;
        int item = GD.RandRange(0, lootTable.Length - 1);
        Item spawnItem = (Item)lootTable[item].Instantiate();
        spawnItem.GlobalPosition = GlobalPosition;
        GetTree().Root.GetNode<Node2D>("DemoLevel").CallDeferred("add_child", spawnItem);
    }
}
