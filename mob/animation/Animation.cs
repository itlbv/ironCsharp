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
        HurtAnimationDelayTimer.Name = "HurtAnimationDelayTimer";
        HurtAnimationDelayTimer.WaitTime = 0.2f;
        AddChild(HurtAnimationDelayTimer);
    }

    public override void _Process(float delta)
    {
        CheckIfOwnerMobIsLoaded(); // TODO quick fix, consider rewriting

        if (Actions.IsIdle() || OwnerMob.IsDead()) {return;}
        
        AbstractAction currentAction = Actions.GetCurrentAction();
            if (currentAction is Move)
            {
                Move moveAction = currentAction as Move;
                SetAnimationDirection(moveAction.VelocityVector);
                AnimationState.Travel("walk");
            }
    }

    private void CheckIfOwnerMobIsLoaded()
    {
        if (Actions is null) 
        {
            if (OwnerMob.Actions is null) {return;}
            Actions = OwnerMob.Actions;
        } 
    }

    public void AnimateHit(Vector2 directionToTarget)
    {
        SetAnimationDirection(directionToTarget);
        AnimationState.Travel("hit");
    }

    public async void AnimateHurt()
    {
        HurtAnimationDelayTimer.Start();
        await ToSignal(HurtAnimationDelayTimer, "timeout");
        AnimationState.Travel("hurt");
    }

    public async void AnimateDie()
    {
        HurtAnimationDelayTimer.Start();
        await ToSignal(HurtAnimationDelayTimer, "timeout");
        AnimationState.Travel("die");
    }

    private void SetAnimationDirection(Vector2 direction)
    {
        AnimationTree.Set("parameters/idle/blend_position", direction);
        AnimationTree.Set("parameters/walk/blend_position", direction);
        AnimationTree.Set("parameters/hurt/blend_position", direction);
        AnimationTree.Set("parameters/hit/blend_position", direction);
        AnimationTree.Set("parameters/die/blend_position", direction);
    }
}
