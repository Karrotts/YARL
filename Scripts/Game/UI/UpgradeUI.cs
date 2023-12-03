using Godot;
using Godot.Collections;
using System;

public partial class UpgradeUI : CanvasLayer
{
    [Export]
    public PackedScene UpgradeOption;

    [Export]
    public Array<Upgrade> UpgardeOptions;

    private HBoxContainer _upgradeContainer;

    public override void _Ready()
    {
        _upgradeContainer = GetNode<HBoxContainer>("UpgradeContainer");
        SpawnUpgradeOptions();
    }

    public override void _Process(double delta)
    {
        NormalizeUpgradeSize();
    }

    private void SpawnUpgradeOptions()
    {
        foreach (Upgrade upgrade in UpgardeOptions)
        {
            UpgradeOption upgradeOption = (UpgradeOption)UpgradeOption.Instantiate();
            upgradeOption.Upgrade = upgrade;
            _upgradeContainer.AddChild(upgradeOption);
        }
    }

    private void NormalizeUpgradeSize()
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
