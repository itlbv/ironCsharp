using Godot;
using System.Collections.Generic;

public class Behaviour : Node2D
{
    private Mob OwnerMob;

    private List<Mob> Mobs;

    private Timer ReevaluateTimer;

    public override void _Ready()
    {
        OwnerMob = (Mob) Owner;    
        Mobs = GetTree().Root.GetNode<Game>("Game").Mobs;
        
        ReevaluateTimer = new Timer();
        ReevaluateTimer.Name = "ReevaluateTimer";
        ReevaluateTimer.Connect("timeout", this, nameof(ReevaluateTimerTimeout));
        AddChild(ReevaluateTimer);
        ReevaluateTimer.WaitTime = 1;
        ReevaluateTimer.Start();
    }

    private void MakeDecision()
    {
        AttackClosestMob();
    }

    private void AttackClosestMob()
    {
        if (Mobs.Count < 2) {return;}

        OwnerMob.Log("choosing closest mob");

        Mob closestMob = null;
        float distance = float.MaxValue;
        foreach (Mob mob in Mobs)
        {
            if (mob == OwnerMob || mob.IsDead()) {continue;}
            
            float distanceToMob = OwnerMob.Position.DistanceTo(mob.Position);
            if (distanceToMob < distance) 
            {
                distance = distanceToMob;
                closestMob = mob;
            }
        }       

        if (closestMob != null)
        {
            OwnerMob.AttackMob(closestMob);
        }
    }

    private void ReevaluateTimerTimeout()
    {
        MakeDecision();
    }
}
