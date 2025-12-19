namespace JakubWegner.UIEngine {

    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Button))]
    public class ButtonInspector : PanelInspector {
        public override void OnInspectorGUI() {
            serializedObject.Update();

            // events
            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onClick"));

            EditorGUILayout.Space(8);

            // states
            DrawState(serializedObject.FindProperty("idleState"), "Idle");
            EditorGUILayout.Space(8);
            DrawState(serializedObject.FindProperty("hoveredState"), "Hovered");
            EditorGUILayout.Space(8);
            DrawState(serializedObject.FindProperty("pressedState"), "Pressed");

            serializedObject.ApplyModifiedProperties();
        }
    }
}
