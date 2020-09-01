using Godot;
using System.Diagnostics;

public class Fight : AbstractAction
{
    private const float ATTACK_TIME = 1;
    
    private Timer AttackTimer;
    RandomNumberGenerator RandomNumber = new RandomNumberGenerator();

    public Fight(Mob ownerMob, Mob targetMob) : base(ownerMob, targetMob)
    {
        OwnerMob = ownerMob;
        TargetMob = targetMob;
        
        AttackTimer = new Timer();
        AttackTimer.Connect("timeout", this, nameof(AttackTimerTimeout));
        AddChild(AttackTimer);
    }

    public override void Do()
    {
        if (AttackTimer.IsStopped())
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        //OwnerMob.Log("hit " + TargetMob.Name);
        OwnerMob.Animation.AnimateHit(GetDirectionToTarget());
        TargetMob.Defend();

        SetTimeToNextAttack();
    }

    public void SetTimeToNextAttack()
    {
        float timeOffset = RandomNumber.RandfRange(-0.2f, 0.2f);
        AttackTimer.Start(ATTACK_TIME + timeOffset);
    }

    private void AttackTimerTimeout()
    {
        PerformAttack();
    }
}
