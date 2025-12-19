namespace JakubWegner.UIEngine {
    using UnityEngine;

    [System.Serializable]
    public struct PanelVisualState {
        public Color fillColor;

        public float cornerRadius;

        public bool enableBorder;
        public Color borderColor;
        public float borderSize;

        public bool enableShadow;
        public Color shadowColor;
        public Vector2 shadowOffset;
    }
}