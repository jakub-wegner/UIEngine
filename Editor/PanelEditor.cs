namespace JakubWegner.UIEngine {

    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Panel))]
    public class PanelInspector : Editor {

        private SerializedProperty state;

        private SerializedProperty fillColor;
        private SerializedProperty cornerRadius;

        private SerializedProperty enableBorder;
        private SerializedProperty borderColor;
        private SerializedProperty borderSize;

        private SerializedProperty enableShadow;
        private SerializedProperty shadowColor;
        private SerializedProperty shadowOffset;

        private void OnEnable() {
            state = serializedObject.FindProperty("state");

            fillColor = state.FindPropertyRelative("fillColor");
            cornerRadius = state.FindPropertyRelative("cornerRadius");

            enableBorder = state.FindPropertyRelative("enableBorder");
            borderColor = state.FindPropertyRelative("borderColor");
            borderSize = state.FindPropertyRelative("borderSize");

            enableShadow = state.FindPropertyRelative("enableShadow");
            shadowColor = state.FindPropertyRelative("shadowColor");
            shadowOffset = state.FindPropertyRelative("shadowOffset");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            // Fill
            EditorGUILayout.LabelField("Fill", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(fillColor, new GUIContent("Fill Color"));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(8);

            // Corners
            EditorGUILayout.LabelField("Corners", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            cornerRadius.floatValue = Mathf.Max(
                EditorGUILayout.FloatField("Radius", cornerRadius.floatValue),
                0f
            );
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(8);

            // Border
            EditorGUILayout.LabelField("Border", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(enableBorder, new GUIContent("Enable Border"));
            if (enableBorder.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(borderColor, new GUIContent("Border Color"));
                borderSize.floatValue = Mathf.Max(
                    EditorGUILayout.FloatField("Border Size", borderSize.floatValue),
                    0f
                );
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(8);

            // Shadow
            EditorGUILayout.LabelField("Shadow", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(enableShadow, new GUIContent("Enable Shadow"));
            if (enableShadow.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(shadowColor, new GUIContent("Shadow Color"));
                EditorGUILayout.PropertyField(shadowOffset, new GUIContent("Shadow Offset"));
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(8);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
