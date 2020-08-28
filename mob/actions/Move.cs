using Godot;

public class Move : AbstractAction
{
    Navigation2D NavigationMap;
    Line2D DebugPathLine;

    public Move(Mob ownerMob, Mob targetMob) : base(ownerMob, targetMob)
    {
        OwnerMob = ownerMob;
        TargetMob = targetMob;
        NavigationMap = OwnerMob.GetNode<Navigation2D>("/root/Game/NavigationMap");
        DebugPathLine = OwnerMob.GetNode<Line2D>("/root/Game/UI/DebugPathLine");
    }

    public override void Do()
    {
        Vector2[] _path = NavigationMap.GetSimplePath(OwnerMob.Position, TargetMob.Position, false);
        DebugPathLine.Points = _path;
    }
}
