namespace JakubWegner.UIEngine {
    using UnityEngine;

    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class Panel : MonoBehaviour {
        // shader

        private static Shader panelShader;
        private static Shader PanelShader {
            get {
                if (!panelShader)
                    panelShader = Shader.Find("JakubWegner/UIEngine/Panel");
                return panelShader;
            }
        }

        // property ids

        private static readonly int FillColorID = Shader.PropertyToID("_FillColor");
        private static readonly int CornerRadiusID = Shader.PropertyToID("_CornerRadius");

        private static readonly int EnableBorderID = Shader.PropertyToID("_EnableBorder");
        private static readonly int BorderColorID = Shader.PropertyToID("_BorderColor");
        private static readonly int BorderSizeID = Shader.PropertyToID("_BorderSize");

        private static readonly int EnableShadowID = Shader.PropertyToID("_EnableShadow");
        private static readonly int ShadowColorID = Shader.PropertyToID("_ShadowColor");
        private static readonly int ShadowOffsetID = Shader.PropertyToID("_ShadowOffset");

        private static readonly int SizeID = Shader.PropertyToID("_Size");
        private static readonly int RectSizeID = Shader.PropertyToID("_RectSize");

        // runtime state

        private bool initialized = false;

        private RectTransform rectTF;
        private Image image;
        private Material material;

        // serialized properties

        [SerializeField] private Color fillColor = Color.white;

        [SerializeField] private float cornerRadius = 20f;

        [SerializeField] private bool enableBorder = false;
        [SerializeField] private Color borderColor = Color.black;
        [SerializeField] private float borderSize = 5f;

        [SerializeField] private bool enableShadow = false;
        [SerializeField] private Color shadowColor = new Color(0f, 0f, 0f, .75f);
        [SerializeField] private Vector2 shadowOffset = Vector2.one * 5f; 

        private void OnRectTransformDimensionsChange() {
            UpdateMaterial();
        }
        private void OnValidate() {
            UpdateMaterial();
        }

        private void Initialize() {
            initialized = true;

            rectTF = GetComponent<RectTransform>();
            image = GetComponent<Image>();

            CreateMaterial();
        }
        private void CreateMaterial() {
            material = new Material(PanelShader);
            material.hideFlags = HideFlags.HideAndDontSave;
            image.material = material;
        }

        public void UpdateMaterial() {
            if (!initialized)
                Initialize();
            if (!material || material.shader != PanelShader)
                CreateMaterial();

            // fill
            material.SetColor(FillColorID, fillColor);

            // corners
            material.SetFloat(CornerRadiusID, cornerRadius);

            // border
            material.SetFloat(EnableBorderID, enableBorder ? 1f : 0f);
            if (enableBorder) {
                material.SetColor(BorderColorID, borderColor);
                material.SetFloat(BorderSizeID, borderSize);
            }

            // shadow
            float rectOffset = 0f;
            material.SetFloat(EnableShadowID, enableShadow ? 1f : 0f);
            if (enableShadow) {
                rectOffset = Mathf.Max(Mathf.Abs(shadowOffset.x), Mathf.Abs(shadowOffset.y));
                material.SetColor(ShadowColorID, shadowColor);
                material.SetVector(ShadowOffsetID, new Vector4(shadowOffset.x, shadowOffset.y));
            }

            // size
            image.UpdateMesh(rectOffset);
            material.SetVector(SizeID, new Vector4(rectTF.rect.width, rectTF.rect.height, 0f, 0f));
            material.SetVector(RectSizeID, new Vector4(rectTF.rect.width + rectOffset * 2f, rectTF.rect.height + rectOffset * 2f, 0f, 0f));
        }
    }

}