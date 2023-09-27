using Godot;
using System;

public partial class GroundState : State
{
	[Export]
	public float JumpVelocity = -150.0f;

	private State AirState;
	private State AttackState;
	private Timer _bufferTimer;

	[Export]
	private string JumpAnimation = "jump_start";
	[Export]
	private string AttackAnimation = "attack1";

    public override void _Ready()
    {
		AirState = GetParent().GetNode("Air") as State;
		AttackState = GetParent().GetNode("Attack") as State;
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
		Playback.Travel(JumpAnimation);
		NextState = AirState;
	}

	private void Attack() {
		Playback.Travel(AttackAnimation);
		NextState = AttackState;
	}

	public override void StateProcess(double delta) {
        if (!Character.IsOnFloor() && _bufferTimer.IsStopped()) {
			NextState = AirState;
		}
	}
}
