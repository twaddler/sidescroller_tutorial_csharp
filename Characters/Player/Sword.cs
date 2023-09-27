using Godot;
using System;

public partial class Sword : Area2D
{
	[Export]
	private int Damage = 10;
	private Player _player;
	private FacingCollisionShape2D _collisionShape2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Monitoring = false;
		BodyEntered += OnBodyEntered;
		_player = (Player)GetParent();
		_collisionShape2D = GetNode<FacingCollisionShape2D>("CollisionShape2D");
		_player.FacingDirectionChanged += OnFacingDirectionChanged;
	}

    private void OnFacingDirectionChanged(bool facingRight)
    {
        _collisionShape2D.Position = facingRight ? _collisionShape2D.FacingRightPosition : _collisionShape2D.FacingLeftPosition;
    }


    private void OnBodyEntered(Node2D body)
    {
		foreach (Node child in body.GetChildren()) {
			if (child is Damageable) {

				Vector2 directionToDamageable = body.GlobalPosition - ((Node2D)GetParent()).GlobalPosition;

				int direction = MathF.Sign(directionToDamageable.X);
				
				if (direction > 0) {
					((Damageable)child).Hit(Damage, Vector2.Right);
				}
				else if (direction < 0) {
					((Damageable)child).Hit(Damage, Vector2.Left);
				}
				else {
					((Damageable)child).Hit(Damage, Vector2.Zero);
				}
			}
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
