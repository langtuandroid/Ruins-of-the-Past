using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GDSGraphView : GraphView {
    public GDSGraphView() {
        addManipulators();
        addGridBackground();

        addStyles();
    } 

    private void addManipulators() {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        
        this.AddManipulator(createNodeContextualMenu("Add node (Single Choice)", GDSDialogueType.SingleChoice));
        this.AddManipulator(createNodeContextualMenu("Add node (Multiple Choice)", GDSDialogueType.MultipleChoice));
    }

    private IManipulator createNodeContextualMenu(string actionTitle, GDSDialogueType dialogueType) {
        ContextualMenuManipulator contextualMenuManipulator = new ContextualMenuManipulator(
            menuEvent => menuEvent.menu.AppendAction(actionTitle, actionEvent => AddElement(createNode(dialogueType, actionEvent.eventInfo.localMousePosition)))
            );
        
        return contextualMenuManipulator;
    }

    private GDSNode createNode(GDSDialogueType dialogueType, Vector2 position) {
        GDSNode node = dialogueType switch {
            GDSDialogueType.SingleChoice => new GDSSingleChoiceNode(),
            GDSDialogueType.MultipleChoice => new GDSMultipleChoiceNode(),
            _ => throw new ArgumentOutOfRangeException(nameof(dialogueType), dialogueType, null)
        };

        node.Initialize(position);
        node.Draw();

        return node;
    }
    
    #region Styles/UI implementation
    
        private void addGridBackground() {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0, gridBackground);
        }
        
        private void addStyles() {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("Assets/Giangs Dialogue System/EditorUSS/GDSGraphViewStyles.uss");
            styleSheets.Add(styleSheet);
        }
        
    #endregion
}
