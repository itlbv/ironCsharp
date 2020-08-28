using Godot;
using System.Collections.Generic;

public class Actions : Node2D
{
    private Stack<AbstractAction> ActionsStack = new Stack<AbstractAction>();
    private Mob OwnerMob;

    public override void _Ready()
    {
        OwnerMob = (Mob) Owner;
    }

    public void Do()
    {
        if (ActionsStack.Count == 0){return;}
        ActionsStack.Peek().Do();
    }

    public void AddMoveToMob(Mob targetMob)
    {
        var move = new Move(OwnerMob, targetMob);
        AddChild(move);
        ActionsStack.Push(move);
    }
}
