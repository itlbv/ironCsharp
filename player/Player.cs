using Godot;
using System;

public class Player : KinematicBody2D
{
    private int speed = 100;
    private AnimationPlayer animationPlayer;

    public override void _Ready(){
        animationPlayer = GetNode<AnimationPlayer>("Animation/AnimationPlayer");
    }

    public override void _PhysicsProcess(float delta) {
        var velocity = Vector2.Zero;
        velocity.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");    
        velocity.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        if (velocity == Vector2.Zero)
        {
            animationPlayer.Play("idle_right");
        } 
        else 
        {
            animationPlayer.Play("walk_right");
        }

        MoveAndSlide(velocity.Normalized() * speed);
    }
}
