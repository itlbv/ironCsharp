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

    public void LeftClickOnMob(Mob mob){
        _selectedMob = mob;
        GD.Print("adfdaf");
        GetNode<SelectionMarker>("UI/SelectionMarker").Mob = mob;
    }

    public void RightCLickOnMob(Mob mob){
        GD.Print("adfasffsafas");
        if (_selectedMob == null || _selectedMob == mob) {return;}
        //_selectedMob.move_to(mob);
    }
}
