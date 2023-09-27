using Godot;
using System;

public partial class Snail : CharacterBody2D
{
	[Export]
	private float _speed = 50.0f;
	[Export]
	private Vector2 _startDirection = new Vector2(-1, 0);

	private Sprite2D _sprite;
	private CharacterStateMachine _stateMachine;
	private State _hitState;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
		_stateMachine = GetNode<CharacterStateMachine>("CharacterStateMachine");
		_sprite = GetNode<Sprite2D>("Sprite");
		_hitState = _stateMachine.GetNode<State>("Hit");
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = _startDirection;
		if (direction != Vector2.Zero  && _stateMachine.CanMove())
		{
			velocity.X = direction.X * _speed;
		}
		else if (_stateMachine.CurrentState != _hitState)
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
