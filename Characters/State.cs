using Godot;
using System;

[Tool]
public partial class State : Node
{
	[Signal]
	public delegate void InterruptStateEventHandler(State nextState);

	[Export]
	public bool CanMove = true;
	public State NextState;
	public AnimationNodeStateMachinePlayback Playback;
	public AnimationTree animationTree;

	public CharacterBody2D Character;

	public virtual void StateInput(InputEvent inputEvent) {
		return;
	}

	public virtual void StateProcess(double delta) {
		return;
	}

	public virtual void OnEnter() {
		return;
	}

	public virtual void OnExit() {
		return;
	}

	public virtual void OnAnimationTreeAnimationFinished(StringName animName) {
		return;
	}
}
