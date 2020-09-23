using Godot;

public class Mob : KinematicBody2D
{
    public const int SPEED = 100;
    private int HP = 4;

    public Actions Actions;
    public Animation Animation;
    private Behaviour Behaviour;
    
    public override void _Ready()
    {
        Actions = GetNode<Actions>("Actions");
        Animation = GetNode<Animation>("Animation");
        Behaviour = GetNode<Behaviour>("Behaviour");
        GetNode<Label>("UI/LabelName").Text = Name;
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
        HP--;
        if (HP <= 0)
        {
            Die();
            return;
        }
        Animation.AnimateHurt();
        Actions.InterruptCurrentAttack();
    }

    private void Die()
    {
        Log("is dead");
        Animation.AnimateDie();
        Actions.Clear();
        Actions.QueueFree();
        Behaviour.QueueFree();
        GetNode("BodyCollisionShape").QueueFree();
        GetNode<Label>("UI/LabelName").Text = "";
    }

    public bool IsDead()
    {
        return HP <= 0;
    }

    public void Log(string message)
    {
        GD.Print(Name + ": " + message);
    }
}