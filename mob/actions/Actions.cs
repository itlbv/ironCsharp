using Godot;
using System.Collections.Generic;

public class Actions : Node2D
{
    private Mob OwnerMob;
    private Stack<AbstractAction> ActionsStack;

    private AnimationTree AnimationTree;
    private AnimationNodeStateMachinePlayback AnimationState;

    public override void _Ready()
    {
        OwnerMob = (Mob) Owner;
        ActionsStack = new Stack<AbstractAction>();
        AnimationTree = OwnerMob.GetNode<AnimationTree>("Animation/AnimationTree");
        AnimationState = AnimationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
    }

    public void Do()
    {
        if (ActionsStack.Count == 0){return;}

        if (ActionsStack.Peek().Finished)
        {
            ActionsStack.Pop();
        }

        if (ActionsStack.Count == 0){return;}
        
        ActionsStack.Peek().Do();
    }

    public void AddMoveToMob(Mob targetMob)
    {
        Move move = new Move(OwnerMob, targetMob);
        AddChild(move);
        ActionsStack.Push(move);
    }

    public void AddMoveToAndFightMob(Mob targetMob)
    {
        ActionsStack.Clear();
        Fight fight = new Fight(OwnerMob, targetMob);
        AddChild(fight);
        ActionsStack.Push(fight);
        Move move = new Move(OwnerMob, targetMob);
        AddChild(move);
        ActionsStack.Push(move);
    }

    public void SetAnimation()
    {
        if (ActionsStack.Count > 0)
        {
            AbstractAction currentAction = ActionsStack.Peek();
            if (currentAction is Move)
            {
                Move moveAction = currentAction as Move;
                Vector2 currentVelocity = moveAction.VelocityVector;
                if (currentVelocity.Length() > 0)
                {
                    AnimationTree.Set("parameters/idle/blend_position", currentVelocity);
                    AnimationTree.Set("parameters/walk/blend_position", currentVelocity);
                    AnimationState.Travel("walk");
                } 
                else 
                {
                    AnimationState.Travel("idle");
                }
            }
        }
    }
}
