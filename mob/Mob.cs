using Godot;

public class Mob : KinematicBody2D
{
    public const int SPEED = 100;
    public int hp = 500;

    public Actions Actions;
    public Animation Animation;
    
    public override void _Ready()
    {
        Actions = GetNode<Actions>("Actions");
        Animation = GetNode<Animation>("Animation");
    }

    public void MoveToMob(Mob targetMob)
    {
        Actions.AddMoveToMob(targetMob);
    }

    public void AttackMob(Mob targetMob)
    {
        Actions.AddMoveToAndFightMob(targetMob);
    }

    public void Defend()
    {
        Log("defending");
        Animation.AnimateHurt();
        Actions.InterruptCurrentAttack();
    }

    public void Log(string message)
    {
        GD.Print(Name + ": " + message);
    }
}