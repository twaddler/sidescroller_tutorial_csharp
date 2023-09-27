using Godot;
using System;

public partial class HitState : State
{
	[Export]
	private string DeadAnimationName = "dead";
	private Damageable damageable;
	private State DeadState;
	private State WalkState;
	
	private Timer _timer;
	private float KnockbackSpeed = 100.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DeadState = GetParent().GetNode("Dead") as State;
		WalkState = GetParent().GetNode("Walk") as State;
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTimeout;

		damageable = GetParent().GetParent().GetNode<Damageable>("Damageable");
		damageable.OnHit += OnDamageableHit;
	}

    private void OnTimeout()
    {
        NextState = WalkState;
    }


    private void OnDamageableHit(Node node, int damageTaken, Vector2 direction)
    {
		if (damageable.Health > 0) {
        	Character.Velocity = KnockbackSpeed * direction;
        	EmitSignal(SignalName.InterruptState, this);
		} else {
        	EmitSignal(SignalName.InterruptState, DeadState);
			Playback.Travel(DeadAnimationName);
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
