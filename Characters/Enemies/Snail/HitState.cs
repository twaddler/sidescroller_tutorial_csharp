using Godot;
using System;

public partial class HitState : State
{
	[Export]
	private string _deadAnimationName = "dead";
	private Damageable _damageable;
	private State _deadState;
	private State _walkState;
	
	private Timer _timer;
	private float _knockbackSpeed = 100.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_deadState = GetParent().GetNode("Dead") as State;
		_walkState = GetParent().GetNode("Walk") as State;
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTimeout;

		_damageable = GetParent().GetParent().GetNode<Damageable>("Damageable");
		_damageable.OnHit += OnDamageableHit;
	}

    private void OnTimeout()
    {
        NextState = _walkState;
    }


    private void OnDamageableHit(Node node, int damageTaken, Vector2 direction)
    {
		if (_damageable.Health > 0) {
        	Character.Velocity = _knockbackSpeed * direction;
        	EmitSignal(SignalName.InterruptState, this);
		} else {
        	EmitSignal(SignalName.InterruptState, _deadState);
			Playback.Travel(_deadAnimationName);
		}
    }

    public override void OnEnter()
    {
		_timer.Start();
    }

    public override void OnExit()
    {
        Character.Velocity = Vector2.Zero;
    }
}
