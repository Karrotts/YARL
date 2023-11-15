using Godot;
using System;
using System.Collections.Generic;

public partial class CharacterInventory : Node
{
    public Character ActiveCharacter { get; set; }
    public List<IItem> Items;

    public override void _Ready()
    {
        Items = new List<IItem>();
    }

    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.Space))
        {
            ReapplyItems();
        }
    }

    public void AddItem(IItem item)
    {
        item.ApplyItem();
        Items.Add(item);
    }

    public void ReapplyItems()
    {
        foreach (IItem item in Items)
        {
            item.ApplyItem();
        }
    }
}
