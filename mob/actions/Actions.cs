using Godot;
using System;
using System.Collections.Generic;

public class Actions : Node2D
{
    private Mob OwnerMob;

    private Stack<AbstractAction> ActionsStack;

    public override void _Ready()
    {
        OwnerMob = (Mob) Owner;
        ActionsStack = new Stack<AbstractAction>();
    }

    public override void _PhysicsProcess(float delta)
    {
        if (IsIdle()){return;}

        if (GetCurrentAction().Finished)
        {
            GetCurrentAction().QueueFree();
            ActionsStack.Pop();
            if (IsIdle()){return;}
        }

        GetCurrentAction().Do();
    }

    public void AddMoveToMob(Mob targetMob)
    {
        Clear();
        Move move = new Move(OwnerMob, targetMob);
        AddChild(move);
        ActionsStack.Push(move);
    }

    public void AddMoveToAndFightMob(Mob targetMob)
    {
        Clear();
        Fight fight = new Fight(OwnerMob, targetMob);
        AddChild(fight);
        ActionsStack.Push(fight);
        Move move = new Move(OwnerMob, targetMob);
        AddChild(move);
        ActionsStack.Push(move);
    }

    public void InterruptCurrentAttack()
    {
        if (IsIdle()) {return;}

        if (GetCurrentAction() is Fight)
        {
            Fight fightAction = GetCurrentAction() as Fight;
            fightAction.SetTimeToNextAttack();            
        }
    }

    public void Clear()
    {
        foreach (AbstractAction action in ActionsStack)
        {
            action.QueueFree();
        }
        ActionsStack.Clear();
    }

    public AbstractAction GetCurrentAction()
    {
        if (IsIdle()) 
        {
            OwnerMob.Log("ERROR: ActionsStack is empty");
            throw new InvalidOperationException();
        }
        return ActionsStack.Peek();
    }
    
    public bool IsIdle()
    {
        return ActionsStack.Count == 0;
    }
}
