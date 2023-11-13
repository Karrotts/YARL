using Godot;
using System;

public partial class GlobalControls : Node
{
    public override void _Process(double delta)
    {
        // at somepoint this code should trigger a menu toggle. For now lets exit...
        if (Input.IsKeyPressed(Key.Escape))
        {
            GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
            GetTree().Quit();
        }
    }
}
