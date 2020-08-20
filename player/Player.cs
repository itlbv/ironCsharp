using Godot;
using System;

public class Player : KinematicBody2D
{
    public override void _PhysicsProcess(float delta) {
        var velocity = new Vector2();
        velocity.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");    
        velocity.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        MoveAndCollide(velocity);
    }
}
