using Godot;
using System.Collections.Generic;

public class Move : AbstractAction
{
    Navigation2D NavigationMap;

    Vector2 VelocityVector;
    List<Vector2> Path;
    bool UsePath;
    Line2D DebugPathLine;

    public Move(Mob ownerMob, Mob targetMob) : base(ownerMob, targetMob)
    {
        OwnerMob = ownerMob;
        TargetMob = targetMob;
        NavigationMap = OwnerMob.GetNode<Navigation2D>("/root/Game/NavigationMap");
        DebugPathLine = OwnerMob.GetNode<Line2D>("/root/Game/UI/DebugPathLine");
        VelocityVector = Vector2.Zero;
    }

    public override void Do()
    {
        UsePath = true;
        if (UsePath)
        {
            VelocityVector = GetPathVelocityVector();
        }
        else
        {
            VelocityVector = Vector2.Zero;
        }

        OwnerMob.MoveAndSlide(VelocityVector.Normalized() * Mob.SPEED);
    }

    private Vector2 GetPathVelocityVector()
    {
        if (Path == null)
        {
            SetNavigationPath();
        }
        
        if (Path == null) 
        {
            OwnerMob.Log("Path is null after calculation");
        }   

        while (Path.Count > 0)
        {
            if (OwnerMob.Position.DistanceTo(Path[0]) < 5)
            {
                Path.RemoveAt(0);
                continue;
            }
            return OwnerMob.Position.DirectionTo(Path[0]);
        }

        return Vector2.Zero;
    }

     private void SetNavigationPath()
    {
        var pathArray = NavigationMap.GetSimplePath(OwnerMob.Position, TargetMob.Position, false);
        Path = new List<Vector2>(pathArray);
        DebugPathLine.Points = pathArray;
    }
}
