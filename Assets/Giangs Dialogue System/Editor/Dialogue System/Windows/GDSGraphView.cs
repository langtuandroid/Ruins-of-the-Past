using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class GDSGraphView : GraphView {
    public GDSGraphView() {
        addManipulators();
        addGridBackground();

        createNode();
        
        addStyles();
    }

    private void createNode() {
        GDSNode node = new GDSNode();
        AddElement(node);
    }

    private void addManipulators() {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
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
