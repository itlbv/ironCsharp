using Godot;

public class Move : AbstractAction
{
    public override void _Ready()
    {
        GD.Print("Move created");
    }

    public override void Do()
    {
        GD.Print("do move");
    }
}
