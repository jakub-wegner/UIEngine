namespace JakubWegner.UIEngine {
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Panel))]
    public class PanelInspector : Editor {
        public override void OnInspectorGUI() {
            serializedObject.Update();

            DrawState(serializedObject.FindProperty("state"), "Panel");

            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawState(SerializedProperty state, string label) {
            // serialized properties
            SerializedProperty fillColor = state.FindPropertyRelative("fillColor");
            SerializedProperty cornerRadius = state.FindPropertyRelative("cornerRadius");

            SerializedProperty enableBorder = state.FindPropertyRelative("enableBorder");
            SerializedProperty borderColor = state.FindPropertyRelative("borderColor");
            SerializedProperty borderSize = state.FindPropertyRelative("borderSize");

            SerializedProperty enableShadow = state.FindPropertyRelative("enableShadow");
            SerializedProperty shadowColor = state.FindPropertyRelative("shadowColor");
            SerializedProperty shadowOffset = state.FindPropertyRelative("shadowOffset");

            // label
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            // fill
            EditorGUILayout.LabelField("Fill", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(fillColor, new GUIContent("Fill Color"));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(4);

            // corners
            EditorGUILayout.LabelField("Corners", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            cornerRadius.floatValue = Mathf.Max(
                EditorGUILayout.FloatField("Radius", cornerRadius.floatValue),
                0f
            );
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(4);

            // border
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
            EditorGUILayout.Space(4);

            // shadow
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
            EditorGUILayout.Space(4);

            EditorGUILayout.EndVertical();
        }
    }
}
