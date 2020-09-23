using Godot;
using System.Collections.Generic;

public class Game : Node
{
    private Mob SelectedMob;
    PackedScene MobScene = (PackedScene) GD.Load("res://mob/Mob.tscn");
    PackedScene SpruceTreeScene = (PackedScene) GD.Load("res://objects/vegetation/spruce_tree/SpruceTree.tscn");
    public List<Mob> Mobs = new List<Mob>();

    public override void _Ready()
    {
        CreateMobs();
        CreateTrees();
    }

    RandomNumberGenerator RandomNumber = new RandomNumberGenerator();
    public void CreateMobs()
    {
        YSort ySort = GetNode<YSort>("YSort");

        for (int i = 0; i < 20; i++)
        {
            Mob mob = (Mob) MobScene.Instance();
            mob.Name = "Mob" + i.ToString();
            mob.Position = GetRandomPosition();
            ySort.AddChild(mob);
            GetNode("YSort/Mob" + i.ToString() + "/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
            GetNode("YSort/Mob" + i.ToString() + "/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
            Mobs.Add(mob);
        }
    }

    private void CreateTrees()
    {
        YSort ySort = GetNode<YSort>("YSort");

        for (int i = 0; i < 10; i++)
        {
            SpruceTree spruceTree = (SpruceTree) SpruceTreeScene.Instance();
            spruceTree.Name = "SpruceTree" + i.ToString();
            spruceTree.Position = GetRandomPosition();
            ySort.AddChild(spruceTree);
        }
    }

    private Vector2 GetRandomPosition()
    {
        float rightX = 50;
        float leftX = 580;
        float upY = 50;
        float downY = 430;

        float x = RandomNumber.RandfRange(rightX, leftX);
        float y = RandomNumber.RandfRange(upY, downY);
        
        return new Vector2(x, y);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        RemoveDeadMobsFromList();
    }

    private void RemoveDeadMobsFromList()
    {
        for (int i = 0; i < Mobs.Count; i++)
        {
            if (Mobs[i].IsDead())
            {
                Mobs.RemoveAt(i);
                GD.Print("Mob removed");
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
