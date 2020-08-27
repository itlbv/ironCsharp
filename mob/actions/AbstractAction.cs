using Godot;
using System;

public abstract class AbstractAction : Node2D
{
    public override void _Ready()
    {
        
    }

    public abstract void Do();
}
