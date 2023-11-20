using Godot;
using System;

public partial class Item : Area2D, IItem
{
    public override void _Ready()
    {
        GetNode<Timer>("DespawnTimer").Timeout += () => QueueFree();
    }

    public virtual void ApplyItem()
    {
        GD.Print("Item has not been implemented yet...");
        throw new NotImplementedException();
    }
}
