using Godot;

public class Animation : Node2D
{
    Mob OwnerMob;

    Actions Actions;

    private AnimationTree AnimationTree;
    private AnimationNodeStateMachinePlayback AnimationState;

    private Timer HurtAnimationDelayTimer;

    public override void _Ready()
    {
        OwnerMob = (Mob) Owner;

        Actions = OwnerMob.Actions;

        AnimationTree = OwnerMob.GetNode<AnimationTree>("Animation/AnimationTree");
        AnimationState = AnimationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;

        HurtAnimationDelayTimer = new Timer();
        HurtAnimationDelayTimer.WaitTime = 0.2f;
        AddChild(HurtAnimationDelayTimer);
    }

    public override void _Process(float delta)
    {
        if (Actions is null) 
        {
            if (OwnerMob.Actions is null) {return;}
            Actions = OwnerMob.Actions;
        } 
        if (Actions.IsIdle()) {return;}
        
        AbstractAction currentAction = Actions.GetCurrentAction();
            if (currentAction is Move)
            {
                Move moveAction = currentAction as Move;
                Vector2 currentVelocity = moveAction.VelocityVector;
                if (currentVelocity.Length() > 0)
                {
                    OwnerMob.Log("moving");
                    AnimationTree.Set("parameters/idle/blend_position", currentVelocity);
                    AnimationTree.Set("parameters/walk/blend_position", currentVelocity);
                    AnimationTree.Set("parameters/hurt/blend_position", currentVelocity);
                    AnimationTree.Set("parameters/hit/blend_position", currentVelocity);
                    AnimationState.Travel("walk");
                } 
                else 
                {
                    AnimationState.Travel("idle");
                }
            }
    }

    public void AnimateHit()
    {
        AnimationState.Travel("hit");
    }

    public async void AnimateHurt()
    {
        HurtAnimationDelayTimer.Start();
        await ToSignal(HurtAnimationDelayTimer, "timeout");
        AnimationState.Travel("hurt");
    }
}
