using Godot;
using System;

public partial class GroundState : State
{
	[Export]
	public float JumpVelocity = -150.0f;
	[Export]
	private string _jumpAnimation = "jump_start";
	[Export]
	private string _attackAnimation = "attack1";

	private State _airState;
	private State _attackState;
	private Timer _bufferTimer;

    public override void _Ready()
    {
		_airState = GetParent().GetNode("Air") as State;
		_attackState = GetParent().GetNode("Attack") as State;
		_bufferTimer = GetNode<Timer>("Timer");
    }

    public override void StateInput(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("jump")) {
			Jump();
		}
		if (inputEvent.IsActionPressed("attack")) {
			Attack();
		}
    }

	private void Jump() {
		Vector2 velocity = Character.Velocity;
		velocity.Y = JumpVelocity;
		Character.Velocity = velocity;
		Playback.Travel(_jumpAnimation);
		NextState = _airState;
	}

	private void Attack() {
		Playback.Travel(_attackAnimation);
		NextState = _attackState;
	}

	public override void StateProcess(double delta) {
        if (!Character.IsOnFloor() && _bufferTimer.IsStopped()) {
			NextState = _airState;
		}
	}
}
