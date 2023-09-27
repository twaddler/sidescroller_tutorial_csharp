using Godot;
using System;

public partial class HealthChangedLabel : Label
{
	private Vector2 _floatSpeed = new Vector2(0, -50);
	private Timer _timer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");
		_timer.Timeout += OnTimeout;
	}

    private void OnTimeout()
    {
        QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		Position += _floatSpeed * (float)delta;
	}
}
