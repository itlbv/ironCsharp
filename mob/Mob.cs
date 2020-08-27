using Godot;

public class Mob : KinematicBody2D
{
    Actions Actions;

    public override void _Ready()
    {
        Actions = GetNode<Actions>("Actions");
    }

    public void MoveToMob(Mob targetMob)
    {
        GD.Print("move to mob");
        var _navigationMap = GetNode<Navigation2D>("/root/Game/NavigationMap");
        var _debugPathLine = GetNode<Line2D>("/root/Game/UI/DebugPathLine");
        Vector2[] _path = _navigationMap.GetSimplePath(Position, targetMob.Position, false);
        _debugPathLine.Points = _path;
    }
}