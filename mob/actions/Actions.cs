using Godot;
using System.Collections.Generic;

public class Actions : Node2D
{
    private Stack<AbstractAction> ActionsStack;
    public override void _Ready()
    {
        GD.Print("Actions created");
        Move move = new Move();
        ActionsStack = new Stack<AbstractAction>();
        ActionsStack.Push(move);
    }

    public void Do()
    {
        if (ActionsStack.Count == 0){return;}

        ActionsStack.Peek().Do();
    }
}
