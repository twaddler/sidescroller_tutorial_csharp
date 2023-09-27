using Godot;
using System;

public partial class StateDebugLabel : Label
{
	private CharacterStateMachine StateMachine;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StateMachine = GetParent().GetNode<CharacterStateMachine>("CharacterStateMachine");
	}

    public override void _Process(double delta)
    {
		if (StateMachine.CurrentState != null) {
        	Text = "State: " + StateMachine.CurrentState.Name;	
		} else {
			Text = "State: Null";
		}
	}
}
