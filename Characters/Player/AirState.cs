using Godot;
using System;

public partial class AirState : State
{
	
	[Export]
	public float DoubleJumpVelocity = -100.0f;
	private bool HasDoubleJumped = false;

	public State LandingState;

	[Export]
	private string DoubleJumpAnimation = "double_jump";
	[Export]
	private string LandingAnimation = "jump_end";

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
		velocity.Y = DoubleJumpVelocity;
		Character.Velocity = velocity;
		Playback.Travel(DoubleJumpAnimation);
		HasDoubleJumped = true;
	}

    public override void OnExit()
    {
		if (NextState == LandingState) {
        	HasDoubleJumped = false;
			Playback.Travel(LandingAnimation);
		}
    }

    public override void StateInput(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("jump") && !HasDoubleJumped) {
			DoubleJump();
		}
    }
}
