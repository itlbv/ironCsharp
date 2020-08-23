using Godot;

public class Game : Node
{
    private KinematicBody2D _selectedMob;

    public override void _Ready()
    {
        GetNode("Mob/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("Mob/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
        GetNode("Mob2/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("Mob2/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
    }

    public void LeftClickOnMob(KinematicBody2D mob){
        GD.Print("left click on mob signal");
        _selectedMob = mob;
    }

    public void RightCLickOnMob(KinematicBody2D mob){
        GD.Print("right click on mob signal");
    }
}
