using Godot;

public class Mob : KinematicBody2D
{
    public const int SPEED = 100;

    public Actions Actions; // TODO temporarily public for setting animation in Fight action

    public override void _Ready()
    {
        Actions = GetNode<Actions>("Actions");
    }

    public override void _Process(float delta)
    {
        Actions.SetAnimation();
    }

    public override void _PhysicsProcess(float delta)
    {
        Actions.Do();
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
    }

    public void Log(string message)
    {
        GD.Print(Name + ": " + message);
    }
}