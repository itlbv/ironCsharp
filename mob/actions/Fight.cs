using Godot;

public class Fight : AbstractAction
{
    public Fight(Mob ownerMob, Mob targetMob) : base(ownerMob, targetMob)
    {
        OwnerMob = ownerMob;
        TargetMob = targetMob;
    }

    public override void Do()
    {
        //OwnerMob.Actions.AnimationState.Travel("hit");
    }
}
