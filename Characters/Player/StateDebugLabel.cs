using Godot;
using System;

public partial class StateDebugLabel : Label
{
	private CharacterStateMachine _stateMachine;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_stateMachine = GetParent().GetNode<CharacterStateMachine>("CharacterStateMachine");
	}

    public override void _Process(double delta)
    {
		if (_stateMachine.CurrentState != null) {
        	Text = "State: " + _stateMachine.CurrentState.Name;	
		} else {
			Text = "State: Null";
		}
	}
}
