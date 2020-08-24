using Godot;

public class SelectionMarker : Node2D
{
    public Mob Mob;

    public override void _Process(float delta)
    {
        Update();
    }

    public override void _Draw()
    {
        if (Mob != null)
        {
            var _pos = new Vector2(Mob.Position.x - 12, Mob.Position.y - 30);
            var _rect = new Rect2(_pos, new Vector2(24, 33));
            var _color = new Color(1, 0, 0, 1);
            DrawRect(_rect, _color, false);
        }
    }
}
