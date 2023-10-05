using UnityEditor;
using UnityEngine.UIElements;

public class GDSEditorWindow : EditorWindow {
    [MenuItem("Window/Giangs Dialogue System/Dialogue Graph")]
    public static void Open() {
        GetWindow<GDSEditorWindow>("Dialogue Graph");
    }
    
    private void OnEnable() {
        addGraphView();
        addStyles();
    }

    private void addStyles() { 
        StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("Assets/Giangs Dialogue System/Editor/Dialogue System/GDSVariables.uss"); 
        rootVisualElement.styleSheets.Add(styleSheet);
    }

    private void addGraphView() {
        GDSGraphView graphView = new GDSGraphView();
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }
}
