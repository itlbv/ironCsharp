using Godot;

public class Game : Node
{
    private Mob SelectedMob;

    public override void _Ready()
    {
        GetNode("Mob/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("Mob/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
        GetNode("Mob2/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("Mob2/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
        GetNode("Mob3/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("Mob3/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
        GetNode("Mob4/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("Mob4/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
        GetNode("Mob5/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("Mob5/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
        GetNode("Mob6/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("Mob6/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
    }

    public void LeftClickOnMob(Mob mobClicked){
        SelectedMob = mobClicked;
        GetNode<SelectionMarker>("UI/SelectionMarker").Mob = mobClicked;
    }

    public void RightCLickOnMob(Mob mobClicked)
    {
        if (SelectedMob == null || 
        SelectedMob == mobClicked || 
        SelectedMob.IsDead()) 
        {
            return;
        }

        SelectedMob.AttackMob(mobClicked);
    }
}
