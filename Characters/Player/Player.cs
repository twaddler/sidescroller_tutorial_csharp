using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed = 200.0f;

	[Export]
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private Sprite2D _sprite;
	private CharacterStateMachine _stateMachine;
	private AnimationTree _animationTree; 

	[Signal]
	public delegate void FacingDirectionChangedEventHandler(bool facingRight);

	public override void _Ready() {
		_stateMachine = GetNode<CharacterStateMachine>("CharacterStateMachine");
		_sprite = GetNode<Sprite2D>("Sprite");
		_animationTree = GetNode<AnimationTree>("AnimationTree");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor()) {
			velocity.Y += Gravity * (float)delta;
		} 

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero && _stateMachine.CanMove())
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		UpdateAnimations(direction);
		MoveAndSlide();
	}

	private void UpdateAnimations(Vector2 direction) {
		_animationTree.Set("parameters/Move/blend_position", direction.X);
		if (direction.X != 0) {
			_sprite.FlipH = direction.X < 0;
		}

		EmitSignal(SignalName.FacingDirectionChanged, !_sprite.FlipH);
	}
}
