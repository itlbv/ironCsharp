using Godot;

public class Game : Node
{
    private Mob _selectedMob;

    public override void _Ready()
    {
        GetNode("Mob/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("Mob/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
        GetNode("Mob2/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("Mob2/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
    }

    public void LeftClickOnMob(Mob mobClicked){
        _selectedMob = mobClicked;
        GetNode<SelectionMarker>("UI/SelectionMarker").Mob = mobClicked;
    }

    public void RightCLickOnMob(Mob mobClicked){
        if (_selectedMob == null || _selectedMob == mobClicked) {return;}
        _selectedMob.MoveToMob(mobClicked);
    }
}
