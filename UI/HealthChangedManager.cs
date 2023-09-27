using Godot;
using System;

public partial class HealthChangedManager : Control
{
	[Export]
	private string _labelPath = "res://UI/health_changed_label.tscn";
	[Export]
	private Color _damageColor = Colors.Red;
	
	private SignalBus _sigBus;
	private Label _healthChangedLabel;

    public override void _Ready()
    {
		_sigBus = GetNode<SignalBus>("/root/SignalBus");
        _sigBus.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(Node node, int AmountChanged)
    {
		_healthChangedLabel = (Label)ResourceLoader.Load<PackedScene>(_labelPath).Instantiate();
		node.AddChild(_healthChangedLabel);
		_healthChangedLabel.Text = AmountChanged.ToString();
		_healthChangedLabel.Modulate = _damageColor;
    }

}
