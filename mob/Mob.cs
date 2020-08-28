using Godot;

public class Mob : KinematicBody2D
{
    Actions Actions;

    public override void _Ready()
    {
        Actions = GetNode<Actions>("Actions");
    }

    public void MoveToMob(Mob targetMob)
    {
        Actions.AddMoveToMob(targetMob);
        Actions.Do();
    }
}