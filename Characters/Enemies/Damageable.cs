using Godot;
using System;

public partial class Damageable : Node
{
	[Signal]
	public delegate void OnHitEventHandler(Node node, int damageTaken, Vector2 direction);

	[Export]
	private int _health = 40;
	public int Health {
		get {
			return _health;
		}
		set {
			sigBus.EmitSignal(SignalBus.SignalName.OnHealthChanged, GetParent(), value - _health);
			_health = value;
		}
	}

	private SignalBus sigBus;

    public override void _Ready()
    {
		sigBus = GetNode<SignalBus>("/root/SignalBus");
    }

    public void Hit(int damage, Vector2 direction) {
		Health -= damage;
		EmitSignal(SignalName.OnHit, GetParent(), damage, direction);
	}
}
