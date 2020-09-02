using Godot;
using System.Collections.Generic;

public class Move : AbstractAction
{
    public Vector2 VelocityVector;

    private Navigation2D NavigationMap;
    private List<Vector2> Path;
    private bool UsePath;
    private Line2D DebugPathLine;

    public Move(Mob ownerMob, Mob targetMob) : base(ownerMob, targetMob, "Move")
    {
        OwnerMob = ownerMob;
        TargetMob = targetMob;
        NavigationMap = OwnerMob.GetNode<Navigation2D>("/root/Game/NavigationMap");
        DebugPathLine = OwnerMob.GetNode<Line2D>("/root/Game/UI/DebugPathLine");
    }

    public override void Do()
    {
        if (CloseToPosition(TargetMob.Position)) 
        {
            Finish();
            return;
        }
        
        SetVelocityVector();
        OwnerMob.MoveAndSlide(VelocityVector);
    }

    private void SetVelocityVector()
    {
        VelocityVector = Vector2.Zero;

        UsePath = true;
        if (UsePath)
        {
            VelocityVector = GetPathVelocityVector();
        }
        else
        {
            return;
        }

        VelocityVector = VelocityVector.Normalized() * Mob.SPEED;
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
            if (CloseToPosition(Path[0]))
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

    private bool CloseToPosition(Vector2 destination)
    {
        return OwnerMob.Position.DistanceTo(destination) < 10;
    }

    public override void Finish()
    {
        Finished = true;
        OwnerMob.Log("finished moving to " + TargetMob.Name);
    }
}
