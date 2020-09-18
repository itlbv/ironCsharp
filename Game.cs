using Godot;
using System.Collections.Generic;

public class Game : Node
{
    private Mob SelectedMob;
    private List<Mob> Mobs = new List<Mob>();

    public override void _Ready()
    {
        CreateMobs();
    }

    public void CreateMobs()
    {
        GetNode("YSort/Mob/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("YSort/Mob/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
        Mobs.Add(GetNode<Mob>("YSort/Mob"));
        
        GetNode("YSort/Mob2/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("YSort/Mob2/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
        Mobs.Add(GetNode<Mob>("YSort/Mob2"));
        
        GetNode("YSort/Mob3/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
        GetNode("YSort/Mob3/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
        Mobs.Add(GetNode<Mob>("YSort/Mob3"));
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (Mobs.Count < 2) {return;}
        foreach (Mob mob in Mobs)
        {
            Mob closestMob = null;
            if (mob.Actions.IsIdle())
            {
                float distanceToClosestMob = float.PositiveInfinity;
                foreach (Mob mobEnemy in Mobs)
                {
                    if (mobEnemy == mob) {continue;}
                    if (mobEnemy.IsDead()) {continue;}
                    if (mob.Position.DistanceTo(mobEnemy.Position) < distanceToClosestMob)
                    {
                        closestMob = mobEnemy;
                    }
                }
            }

            if (closestMob != null)
            {
                mob.AttackMob(closestMob);
            }
        }

        for (int i = 0; i < Mobs.Count; i++)
        {
            if (Mobs[i].IsDead())
            {
                Mobs.RemoveAt(i);
                i--;
            }
        }
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
