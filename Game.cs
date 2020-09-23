using Godot;
using System.Collections.Generic;

public class Game : Node
{
    private Mob SelectedMob;
    PackedScene MobScene = (PackedScene) GD.Load("res://mob/Mob.tscn");
    private List<Mob> Mobs = new List<Mob>();

    public override void _Ready()
    {
        CreateMobs();
    }

    RandomNumberGenerator RandomNumber = new RandomNumberGenerator();
    public void CreateMobs()
    {
        float rightX = 50;
        float leftX = 580;
        float upY = 50;
        float downY = 430;

        YSort ySort = GetNode<YSort>("YSort");

        for (int i = 0; i < 5; i++)
        {
            float x = RandomNumber.RandfRange(rightX, leftX);
            float y = RandomNumber.RandfRange(upY, downY);

            Mob mob = (Mob) MobScene.Instance();
            mob.Name = "Mob" + i.ToString();
            mob.Position = new Vector2(x, y);
            ySort.AddChild(mob);
            GetNode("YSort/Mob" + i.ToString() + "/UI/SelectionArea").Connect("LeftClick", this, nameof(LeftClickOnMob));
            GetNode("YSort/Mob" + i.ToString() + "/UI/SelectionArea").Connect("RightClick", this, nameof(RightCLickOnMob));
            Mobs.Add(mob);
        }
    }

    public override void _Process(float delta)
    {
        base._Process(delta);
        AssignTargetsToMobs();
    }

    private void AssignTargetsToMobs()
    {
        if (Mobs.Count < 2) {return;}
        foreach (Mob mob in Mobs)
        {
            if (mob.IsDead()){return;}
            if (!mob.Actions.IsIdle()){return;}

            Mob closestMob = null;
            float distanceToClosestMob = float.MaxValue;
            foreach (Mob mobEnemy in Mobs)
            {
                if (mobEnemy == mob) {continue;}
                if (mobEnemy.IsDead()) {continue;}
                if (mob.Position.DistanceTo(mobEnemy.Position) < distanceToClosestMob)
                {
                    closestMob = mobEnemy;
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
