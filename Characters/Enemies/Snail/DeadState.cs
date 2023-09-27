using Godot;
using System;

public partial class DeadState : State
{
	[Export]
	private string _deadAnimationName = "dead";

	public override void OnAnimationTreeAnimationFinished(StringName animName)
    {
	    if (animName == _deadAnimationName) {
			GetParent().GetParent().QueueFree();
		}
    }
}
