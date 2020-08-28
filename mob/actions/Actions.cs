using Godot;
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

    public void Do()
    {
        if (ActionsStack.Count == 0){return;}
        ActionsStack.Peek().Do();
    }

    public void AddMoveToMob(Mob targetMob)
    {
        Move move = new Move(OwnerMob, targetMob);
        AddChild(move);
        ActionsStack.Push(move);
    }
}
