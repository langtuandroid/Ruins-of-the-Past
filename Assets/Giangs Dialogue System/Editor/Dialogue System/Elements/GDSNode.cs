using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GDSNode : Node {
    public string dialogueName { get; set; }
    public List<string> choices { get; set; }
    public string text { get; set; }
    public GDSDialogueType DialogueType { get; set; }

    public virtual void Initialize(Vector2 position) {
        dialogueName = "DialogueName";
        choices = new List<string>();
        text = "Dialogue text.";
        
        SetPosition(new Rect(position, Vector2.zero));
    }

    public virtual void Draw() {
        //Title
        TextField dialogueNameTextField = new TextField {
            value = dialogueName
        };
        titleContainer.Insert(0, dialogueNameTextField);

        //Input
        Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        inputPort.portName = "In";
        inputContainer.Add(inputPort);
        
        //Container Dialogue
        VisualElement customDataContainer = new VisualElement();

        Foldout foldout = new Foldout {
            text = "Dialogue"
        };
        TextField textField = new TextField {
            value = text
        };
        
        foldout.Add(textField);
        customDataContainer.Add(foldout);
        extensionContainer.Add(customDataContainer);
    }
}
