using Godot;
using System;

public partial class DebugUI : CanvasLayer
{
    private Label _fpsLabel;

    public override void _Ready()
    {
        _fpsLabel = GetNode<Label>("FPSLabel");
    }

    public override void _Process(double delta)
    {
        UpdateUIComponents();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        CheckShow();
    }

    private void UpdateUIComponents()
    {
        _fpsLabel.Text = "FPS: " + Engine.GetFramesPerSecond().ToString();
    }

    private void CheckShow()
    {
        if (Input.IsKeyPressed(Key.Quoteleft)) Visible = !Visible;
    }
}
