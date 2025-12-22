namespace JakubWegner.UIEngine {

    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Button))]
    public class ButtonInspector : PanelInspector {
        public override void OnInspectorGUI() {
            serializedObject.Update();

            SerializedProperty isActive = serializedObject.FindProperty("isActive");
            SerializedProperty onClick = serializedObject.FindProperty("onClick");

            EditorGUILayout.PropertyField(isActive, new GUIContent("Is Active"));
            EditorGUILayout.Space(4);
            EditorGUILayout.PropertyField(onClick);

            // states
            DrawState(serializedObject.FindProperty("idleState"), "Idle");
            EditorGUILayout.Space(8);
            DrawState(serializedObject.FindProperty("hoveredState"), "Hovered");
            EditorGUILayout.Space(8);
            DrawState(serializedObject.FindProperty("pressedState"), "Pressed");
            EditorGUILayout.Space(8);
            DrawState(serializedObject.FindProperty("inactiveState"), "Inactive");

            serializedObject.ApplyModifiedProperties();
        }
    }
}
