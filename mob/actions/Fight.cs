using Godot;

public class Fight : AbstractAction
{
    const float ATTACK_TIME = 1;
    private Timer AttackTimer;
    
    RandomNumberGenerator RandomNumber = new RandomNumberGenerator();

    public Fight(Mob ownerMob, Mob targetMob) : base(ownerMob, targetMob)
    {
        OwnerMob = ownerMob;
        TargetMob = targetMob;
        
        AttackTimer = new Timer();
        AttackTimer.Connect("timeout", this, nameof(AttackTimerTimeout));
        AddChild(AttackTimer);
        SetTimeToNextAttack();
    }

    public override void Do()
    {
        if (AttackTimer.IsStopped())
        {
            AttackTimer.Start();
            AttackTimerTimeout();
        }
    }

    private void AttackTimerTimeout()
    {
        OwnerMob.Log("hit " + TargetMob.Name);
        OwnerMob.Animation.AnimateHit(GetDirectionToTarget());
        SetTimeToNextAttack();
        TargetMob.Defend();
    }

    private void SetTimeToNextAttack()
    {
        float timeOffset = RandomNumber.RandfRange(-0.2f, 0.2f);
        AttackTimer.WaitTime = ATTACK_TIME + timeOffset;
        if (AttackTimer.IsStopped())
        {
            AttackTimer.Start(ATTACK_TIME + timeOffset);
        }
    }
}
