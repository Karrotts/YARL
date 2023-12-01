using Godot;
using System;

public partial class UpgradeUI : CanvasLayer
{
    private HBoxContainer _upgradeContainer;

    public override void _Ready()
    {
        _upgradeContainer = GetNode<HBoxContainer>("UpgradeContainer");
    }

    public override void _Process(double delta)
    {
        NormalizeUpgradeSize();
    }

    public void NormalizeUpgradeSize()
    {
        float largestY = 0;
        foreach (Node node in _upgradeContainer.GetChildren())
        {
            if ((node as UpgradeOption).PanelSize.Y > largestY)
            {
                largestY = (node as UpgradeOption).PanelSize.Y;
            }
        }

        foreach (Node node in _upgradeContainer.GetChildren())
        {
            Vector2 newSize = new Vector2((node as UpgradeOption).PanelSize.X, largestY);
            (node as UpgradeOption).PanelSize = newSize;
        }
    }
}
