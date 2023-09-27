using Godot;
using System;

public partial class LandingState : State
{
	[Export]
	private string LandingAnimation = "jump_end";

	private State GroundState;

    public override void _Ready()
    {
		GroundState = GetParent().GetNode<State>("Ground");
    }

    public override void OnEnter()
    {
		Playback.Travel(LandingAnimation);
    }

    public override void OnExit()
    {
    }

    public override void OnAnimationTreeAnimationFinished(StringName animName)
    {
        if (animName == LandingAnimation) {
		 	NextState = GroundState;
		}
    }
}
