using Godot;
using System;

public partial class HealthChangedManager : Control
{
	[Export]
	private string labelPath = "res://UI/health_changed_label.tscn";
	[Export]
	private Color damageColor = Colors.Red;
	private SignalBus sigBus;
	private Label healthChangedLabel;

    public override void _Ready()
    {
		sigBus = GetNode<SignalBus>("/root/SignalBus");
        sigBus.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(Node node, int AmountChanged)
    {
		healthChangedLabel = (Label)ResourceLoader.Load<PackedScene>(labelPath).Instantiate();
		node.AddChild(healthChangedLabel);
		healthChangedLabel.Text = AmountChanged.ToString();
		healthChangedLabel.Modulate = damageColor;
    }

}
