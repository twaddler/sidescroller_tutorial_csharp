using Godot;
using System;

public partial class AttackState : State
{
	[Export]
	private string _primaryAttackAnimation = "attack1";
	[Export]
	private string _followUpAttackAnimation = "attack2";
	
	private bool _hasAttackedTwice = false;
	private State _returnState;
	private Timer _timer;

    public override void _Ready()
    {
		_returnState = GetParent().GetNode("Ground") as State;
		_timer = GetNode<Timer>("FollowUpAttackTimer");
    }

    public override void StateInput(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("attack") && !_hasAttackedTwice) {
			_timer.Start();
		}
    }

    public override void OnExit()
    {
		Playback.Travel("Move");
        _hasAttackedTwice = false;
    }

    public override void OnAnimationTreeAnimationFinished(StringName animName)
    {
		if (animName == _primaryAttackAnimation) {
			if (_timer.IsStopped()) {
				NextState = _returnState;
			} else {
				Playback.Travel(_followUpAttackAnimation);
			}
		}
		if (animName == _followUpAttackAnimation) {
			NextState = _returnState;
		}
    }
}
