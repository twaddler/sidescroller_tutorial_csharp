using Godot;
using System.Collections.Generic;

public partial class CharacterStateMachine : Node
{

	[Export]
	private string initialStateName;
	public State CurrentState;
	private CharacterBody2D CharacterBody;

	private AnimationTree _animationTree { get; set;}

	public List<State> States;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CharacterBody = GetParent<CharacterBody2D>();
		_animationTree = GetParent().GetNode<AnimationTree>("AnimationTree");
		_animationTree.AnimationFinished += OnAnimationTreeAnimationFinished;
		_animationTree.Active = true;
		AnimationNodeStateMachinePlayback _playback = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");

		CurrentState = GetNode<State>(initialStateName);

		// Build list of all possible States
		States = new List<State>();
		foreach (Node child in GetChildren()) {
			if (child is State) {
				((State)child).Character = CharacterBody;
				((State)child).Playback = _playback;
				((State)child).InterruptState += OnInterruptState;
				States.Add((State) child);
			} else {
				GD.Print("Child " + child.Name + " is not a State");
			}
		}
	}

	public override void _PhysicsProcess(double delta) {
		if (CurrentState.NextState != null) {
			SwitchStates(CurrentState.NextState);
		}

		CurrentState.StateProcess(delta);
	}

	private void SwitchStates(State newState) {
		if (CurrentState != null) {
			CurrentState.OnExit();
			CurrentState.NextState = null;
		}

		CurrentState = newState;

		CurrentState.OnEnter();
	}

	public override void _Input(InputEvent inputEvent) {
		CurrentState.StateInput(inputEvent);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public bool CanMove() {
		return CurrentState.CanMove;
	}

	public void OnAnimationTreeAnimationFinished(StringName animName) {
		CurrentState.OnAnimationTreeAnimationFinished(animName);
	}

	public void OnInterruptState(State nextState) {
		SwitchStates(nextState);
	}
}
