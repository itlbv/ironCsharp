using Godot;

public class Fight : AbstractAction
{
    private const float ATTACK_TIME = 1;
    
    private Timer AttackTimer;
    RandomNumberGenerator RandomNumber = new RandomNumberGenerator();

    public Fight(Mob ownerMob, Mob targetMob) : base(ownerMob, targetMob, "Fight")
    {
        OwnerMob = ownerMob;
        TargetMob = targetMob;
        
        AttackTimer = new Timer();
        AttackTimer.Name = "AttackTimer";
        AttackTimer.Connect("timeout", this, nameof(AttackTimerTimeout));
        AddChild(AttackTimer);
    }

    public override void Do()
    {
        if (TargetMob.IsDead())
        {
            Finish();
            return;
        }

        if (TargetIsNotClose())
        {
            OwnerMob.AttackMob(TargetMob);
            return;
        }

        if (AttackTimer.IsStopped())
        {
            PerformAttack();
        }
    }

    private bool TargetIsNotClose()
    {
        return OwnerMob.Position.DistanceTo(TargetMob.Position) > 10;
    }

    private void PerformAttack()
    {
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

    public override void Finish()
    {
        Finished = true;
        AttackTimer.QueueFree();
    }
}
