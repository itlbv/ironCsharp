using Godot;

public abstract class AbstractAction : Node2D
{
    public Mob OwnerMob;
    public Mob TargetMob;

    public bool Finished;

    public AbstractAction(Mob ownerMob, Mob targetMob, string actionName)
    {
        OwnerMob = ownerMob;
        TargetMob = targetMob;
        Name = actionName;
    }

    public abstract void Do();

    public Vector2 GetDirectionToTarget()
    {
        return OwnerMob.Position.DirectionTo(TargetMob.Position);
    }

    public abstract void Finish();
}
