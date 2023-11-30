using Godot;
using System;

public partial class UpgradeOption : Control
{
    [Export]
    public CompressedTexture2D Texture { get; set; }

    [Export(PropertyHint.MultilineText)]
    public string BuffText { get; set; } = "";

    [Export(PropertyHint.MultilineText)]
    public string DebuffText { get; set; } = "";

    [Export]
    public ItemRarity Rarity { get; set; } = ItemRarity.COMMON;

    private Label _buffLabel;
    private Label _debuffLabel;
    private int _labelSize = 16;

    public override void _Ready()
    {
        GetNode<TextureRect>("%UpgradeIcon").Texture = Texture;
        _buffLabel = GetNode<Label>("%BuffLabel");
        _debuffLabel = GetNode<Label>("%DebuffLabel");

        _buffLabel.Text = BuffText;
        _debuffLabel.Text = DebuffText;
    }
}

public enum ItemRarity
{
    COMMON,
    RARE,
    LEGENDARY
}
