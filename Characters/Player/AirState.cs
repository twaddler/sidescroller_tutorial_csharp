using Godot;
using System;

public partial class AirState : State
{
	
	[Export]
	private float _doubleJumpVelocity = -100.0f;
	[Export]
	private string _doubleJumpAnimation = "double_jump";
	[Export]
	private string landingAnimation = "jump_end";

	private bool _hasDoubleJumped = false;

	private State LandingState;

    public override void _Ready()
    {
		LandingState = GetParent().GetNode<State>("Landing");
    }
	public override void StateProcess(double delta) {
		if (Character.IsOnFloor()) {
			NextState = LandingState;
		}
	}

	private void DoubleJump() {
		Vector2 velocity = Character.Velocity;
		velocity.Y = _doubleJumpVelocity;
		Character.Velocity = velocity;
		Playback.Travel(_doubleJumpAnimation);
		_hasDoubleJumped = true;
	}

    public override void OnExit()
    {
		if (NextState == LandingState) {
        	_hasDoubleJumped = false;
			Playback.Travel(landingAnimation);
		}
    }

    public override void StateInput(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("jump") && !_hasDoubleJumped) {
			DoubleJump();
		}
    }
}
