using Godot;

public class SelectionArea : Area2D
{
    [Signal]
    delegate void LeftClick(Mob owner);

    [Signal]
    delegate void RightClick(Mob owner);
    
    public override void _Ready(){
        Connect("input_event", this, nameof(_onSelectionMarkerInputEvent));
    } 
    
    public void _onSelectionMarkerInputEvent(Viewport viewport, InputEventMouse inputEvent, int shapeIdx){
        if (!inputEvent.IsPressed()){return;}
        if (inputEvent.GetType() != typeof(InputEventMouseButton)){return;}
        
        InputEventMouseButton inputEventMouseButton = inputEvent as InputEventMouseButton;
        
        switch (inputEventMouseButton.ButtonIndex)
        {
            case 1:
                EmitSignal(nameof(LeftClick), GetParentMob());
                break;
            case 2:
                EmitSignal(nameof(RightClick), GetParentMob());
                break;
        }
    }

    private Node GetParentMob()
    {
        return GetParent<Node2D>().GetParent<Mob>(); // get UI grouping node -> get Mob
    }
}