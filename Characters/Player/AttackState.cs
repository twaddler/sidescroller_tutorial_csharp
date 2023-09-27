using Godot;
using System;

public partial class AttackState : State
{
	[Export]
	private string PrimaryAttackAnimation = "attack1";
	[Export]
	private string FollowUpAttackAnimation = "attack2";
	private bool HasAttackedTwice = false;
	private State ReturnState;
	private Timer _timer;

    public override void _Ready()
    {
		ReturnState = GetParent().GetNode("Ground") as State;
		_timer = GetNode<Timer>("FollowUpAttackTimer");
    }

    public override void StateInput(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("attack") && !HasAttackedTwice) {
			_timer.Start();
		}
    }

    public override void OnExit()
    {
		Playback.Travel("Move");
        HasAttackedTwice = false;
    }

    public override void OnAnimationTreeAnimationFinished(StringName animName)
    {
		if (animName == PrimaryAttackAnimation) {
			if (_timer.IsStopped()) {
				NextState = ReturnState;
			} else {
				Playback.Travel(FollowUpAttackAnimation);
			}
		}
		if (animName == FollowUpAttackAnimation) {
			NextState = ReturnState;
		}
    }
}
