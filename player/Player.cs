using Godot;
using System;

public class Player : KinematicBody2D
{
    private int speed = 100;
    private AnimationPlayer animationPlayer;
    private AnimationTree animationTree;
    private AnimationNodeStateMachinePlayback animationState;

    public override void _Ready(){
        animationPlayer = GetNode<AnimationPlayer>("Animation/AnimationPlayer");
        animationTree = GetNode<AnimationTree>("Animation/AnimationTree");
        animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
    }

    public override void _PhysicsProcess(float delta) {
        var velocity = Vector2.Zero;
        velocity.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");    
        velocity.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        velocity = velocity.Normalized();

        if (velocity != Vector2.Zero)
        {
            animationTree.Set("parameters/idle/blend_position", velocity);
            animationTree.Set("parameters/walk/blend_position", velocity);
            animationState.Travel("walk");
        } 
        else 
        {
          animationState.Travel("idle");
        }

        MoveAndSlide(velocity * speed);
    }
}
