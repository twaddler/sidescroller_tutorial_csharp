using Godot;

public partial class FacingCollisionShape2D : CollisionShape2D
{
	[Export]
	public Vector2 FacingLeftPosition = Vector2.Left;

	[Export]
	public Vector2 FacingRightPosition = Vector2.Right;
}
