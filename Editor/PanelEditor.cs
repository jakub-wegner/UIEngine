namespace JakubWegner.UIEngine {

    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Panel))]
    public class PanelInspector : Editor {
        private SerializedProperty panelStyle;

        private SerializedProperty fillColor;

        private SerializedProperty cornerRadius;

        private SerializedProperty enableBorder;
        private SerializedProperty borderColor;
        private SerializedProperty borderSize;

        private SerializedProperty enableShadow;
        private SerializedProperty shadowColor;
        private SerializedProperty shadowOffset;

        private void OnEnable() {
            panelStyle = serializedObject.FindProperty(nameof(panelStyle));

            fillColor = serializedObject.FindProperty(nameof(fillColor));

            cornerRadius = serializedObject.FindProperty(nameof(cornerRadius));

            enableBorder = serializedObject.FindProperty(nameof(enableBorder));
            borderColor = serializedObject.FindProperty(nameof(borderColor));
            borderSize = serializedObject.FindProperty(nameof(borderSize));

            enableShadow = serializedObject.FindProperty(nameof(enableShadow));
            shadowColor = serializedObject.FindProperty(nameof(shadowColor));
            shadowOffset = serializedObject.FindProperty(nameof(shadowOffset));
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            // fill
            EditorGUILayout.LabelField("Fill", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(fillColor, new GUIContent("Fill color"));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(8);

            // corners
            EditorGUILayout.LabelField("Corners", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            cornerRadius.floatValue = Mathf.Max(EditorGUILayout.FloatField("Radius", cornerRadius.floatValue), 0f);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(8);

            // border
            EditorGUILayout.LabelField("Border", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(enableBorder, new GUIContent("Enable border"));
            if (enableBorder.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(borderColor, new GUIContent("Border color"));
                borderSize.floatValue = Mathf.Max(EditorGUILayout.FloatField("Border size", borderSize.floatValue), 0f);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(8);

            // shadow
            EditorGUILayout.LabelField("Shadow", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(enableShadow, new GUIContent("Enable shadow"));
            if (enableShadow.boolValue) {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(shadowColor, new GUIContent("Shadow color"));
                EditorGUILayout.PropertyField(shadowOffset, new GUIContent("Shadow offset"));
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(8);

            serializedObject.ApplyModifiedProperties();
        }
    }
}