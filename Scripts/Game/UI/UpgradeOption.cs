using Godot;
using System;

public partial class UpgradeOption : Control
{
    [Signal]
    public delegate void UpgradeItemSelectedEventHandler(int id);

    [Export]
    public int Id;

    [Export]
    public Upgrade Upgrade { get; set; }

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
        GetNode<TextureRect>("%UpgradeIcon").Texture = (Texture2D)Upgrade.Texture;
        _buffLabel = GetNode<Label>("%BuffLabel");
        _debuffLabel = GetNode<Label>("%DebuffLabel");
        _panelContainer = GetNode<PanelContainer>("PanelContainer");

        _buffLabel.Text = Upgrade.UpgradeText;
        _debuffLabel.Text = DebuffText;

        _panelContainer.GetNode<Panel>("Panel").Modulate = DefaultColor;
        _panelContainer.MouseEntered += OnMouseEnter;
        _panelContainer.MouseExited += OnMouseExit;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsMouseButtonPressed(MouseButton.Left) && _highlighted)
        {
            EmitSignal(SignalName.UpgradeItemSelected, Id);
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