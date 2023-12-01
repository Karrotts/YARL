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
    public Color DefaultColor { get; set; }

    [Export]
    public Color HighlightColor { get; set; }

    public Vector2 PanelSize {
        get { return GetNode<PanelContainer>("PanelContainer").Size; }
        set { GetNode<PanelContainer>("PanelContainer").Size = value; }
    }

    private Label _buffLabel;
    private Label _debuffLabel;
    private PanelContainer _panelContainer;
    private bool _highlighted;

    public override void _Ready()
    {
        GetNode<TextureRect>("%UpgradeIcon").Texture = Texture;
        _buffLabel = GetNode<Label>("%BuffLabel");
        _debuffLabel = GetNode<Label>("%DebuffLabel");
        _panelContainer = GetNode<PanelContainer>("PanelContainer");

        _buffLabel.Text = BuffText;
        _debuffLabel.Text = DebuffText;

        _panelContainer.GetNode<Panel>("Panel").Modulate = DefaultColor;
        _panelContainer.MouseEntered += OnMouseEnter;
        _panelContainer.MouseExited += OnMouseExit;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left) && _highlighted)
        {
            GD.Print(BuffText);
            QueueFree();
        }
    }

    public void OnMouseEnter()
    {
        _panelContainer.GetNode<Panel>("Panel").Modulate = HighlightColor;
        _highlighted = true;
    }

    public void OnMouseExit()
    {
        _panelContainer.GetNode<Panel>("Panel").Modulate = DefaultColor;
        _highlighted = false;
    }
}