using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GDSMultipleChoiceNode : GDSNode {
    public override void Initialize(Vector2 position) {
        base.Initialize(position);

        DialogueType = GDSDialogueType.MultipleChoice;
        
        choices.Add("New choice");
    }

    public override void Draw() {
        base.Draw();

        //Main container
        Button addChoice = new Button {
            text = "Add choice"
        };
        
        mainContainer.Insert(1, addChoice);
        
        //output
        foreach (string choice in choices) {
            Port choicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(bool));
            
            choicePort.portName = "";
            
            Button delete = new Button {
                text = "X"
            };
            TextField textField = new TextField {
                value = choice
            };
            textField.style.flexDirection = FlexDirection.Column;
            
            choicePort.Add(textField);
            choicePort.Add(delete);
            outputContainer.Add(choicePort);
            
            RefreshExpandedState();
        }
    }
}
