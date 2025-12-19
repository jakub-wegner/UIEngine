namespace JakubWegner.UIEngine {
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UI;

    public static class PanelCreator {
        [MenuItem("GameObject/UIEngine/Panel", false, 0)]
        public static void CreatePanel() {
            // create gameobject
            GameObject go = new GameObject("Panel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Panel));

            // parent
            GameObject parent = Selection.activeGameObject;
            if (parent != null && parent.GetComponentInParent<Canvas>()) {
                go.transform.SetParent(parent.transform, false);
            }
            else {
                Canvas canvas = Object.FindFirstObjectByType<Canvas>();
                if (!canvas) {
                    // create canvas
                    GameObject canvasGO = new GameObject("UI", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                    canvas = canvasGO.GetComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    CanvasScaler canvasScaler = canvasGO.GetComponent<CanvasScaler>();
                    canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    canvasScaler.referenceResolution = new Vector2(1920, 1080);
                }
                go.transform.SetParent(canvas.transform, false);
            }

            // select gameobject
            Selection.activeGameObject = go;
        }

    }
}
