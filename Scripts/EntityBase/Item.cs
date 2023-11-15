using Godot;
using System;

public partial class Item : Area2D, IItem
{
    public virtual void ApplyItem()
    {
        GD.Print("Item has not been implemented yet...");
        throw new NotImplementedException();
    }
}
