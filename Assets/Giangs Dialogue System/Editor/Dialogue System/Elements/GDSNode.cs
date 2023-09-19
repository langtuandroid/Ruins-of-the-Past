using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class GDSNode : Node {
    public string dialogueName { get; set; }
    public List<string> choices { get; set; }
    public string text { get; set; }
    public GDSDialogueType DialogueType { get; set; }

    public void Initialize()
    {
        dialogueName = "DialogueName";
        choices = new List<string>();
        text = "Dialogue text.";
    }

    public void Draw()
    {
        //Title
        TextField dialogueNameTextField = new TextField
        {
            value = dialogueName
        };
        titleContainer.Insert(0, dialogueNameTextField);

        Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        inputPort.portName = "Dialogue Connection";
        inputContainer.Add(inputPort);
    }
}
