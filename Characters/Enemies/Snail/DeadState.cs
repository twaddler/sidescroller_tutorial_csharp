using Godot;
using System;

public partial class DeadState : State
{
	[Export]
	private string DeadAnimationName = "dead";

	public override void OnAnimationTreeAnimationFinished(StringName animName)
    {
	    if (animName == DeadAnimationName) {
			GetParent().GetParent().QueueFree();
		}
    }
}
