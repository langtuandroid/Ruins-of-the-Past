using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GDSSingleChoiceNode : GDSNode {
    public override void Initialize(Vector2 position) {
        base.Initialize(position);

        DialogueType = GDSDialogueType.SingleChoice;
        
        choices.Add("Next dialogue");
    }

    public override void Draw() {
        base.Draw();

        //output
        foreach (string choice in choices) {
            Port choicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(bool));
            
            choicePort.portName = choice;
            outputContainer.Add(choicePort);
            
            RefreshExpandedState();
        }
    }
}
