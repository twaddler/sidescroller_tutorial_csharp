using Godot;
using System;

public partial class LandingState : State
{
    [Export]
    private string _landingAnimation = "jump_end";

    private State _groundState;

    public override void _Ready()
    {
		  _groundState = GetParent().GetNode<State>("Ground");
    }

    public override void OnEnter()
    {
		  Playback.Travel(_landingAnimation);
    }

    public override void OnExit()
    {
    }

    public override void OnAnimationTreeAnimationFinished(StringName animName)
    {
      if (animName == _landingAnimation) {
		 	  NextState = _groundState;
		  }
    }
}
