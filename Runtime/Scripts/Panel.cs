namespace JakubWegner.UIEngine {
    using UnityEngine;

    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class Panel : MonoBehaviour {
        private static Shader panelShader;
        private static Shader PanelShader {
            get {
                if (!panelShader)
                    panelShader = Shader.Find("JakubWegner/UIEngine/Panel");
                return panelShader;
            }
        }

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

        private bool initialized = false;

        private RectTransform rectTF;
        private Image image;
        private Material material;

        [SerializeField] protected PanelVisualState state;

        private void Reset() {
            state.fillColor = new Color(0.14f, 0.15f, 0.18f, 1f);
            state.cornerRadius = 16f;

            state.enableBorder = true;
            state.borderSize = 1.2f;
            state.borderColor = new Color(0.45f, 0.55f, 1f, 0.25f);

            state.enableShadow = true;
            state.shadowColor = new Color(0f, 0f, 0f, 0.45f);
            state.shadowOffset = new Vector2(0f, -8f);

            UpdateMaterial();
        }

        private void OnRectTransformDimensionsChange() {
            UpdateMaterial();
        }
        private void OnValidate() {
            UpdateMaterial();
        }
        private void Start() {
            initialized = false;
            UpdateMaterial();
        }

        protected void Initialize() {
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

        protected void UpdateMaterial() {
            if (!initialized)
                Initialize();
            if (!material || material.shader != PanelShader)
                CreateMaterial();

            // fill
            material.SetColor(FillColorID, state.fillColor);

            // corners
            material.SetFloat(CornerRadiusID, state.cornerRadius);

            // border
            material.SetFloat(EnableBorderID, state.enableBorder ? 1f : 0f);
            if (state.enableBorder) {
                material.SetColor(BorderColorID, state.borderColor);
                material.SetFloat(BorderSizeID, state.borderSize);
            }

            // shadow
            float rectOffset = 0f;
            material.SetFloat(EnableShadowID, state.enableShadow ? 1f : 0f);
            if (state.enableShadow) {
                rectOffset = Mathf.Max(Mathf.Abs(state.shadowOffset.x), Mathf.Abs(state.shadowOffset.y));
                material.SetColor(ShadowColorID, state.shadowColor);
                material.SetVector(ShadowOffsetID, new Vector4(state.shadowOffset.x, state.shadowOffset.y));
            }

            // size
            image.UpdateMesh(rectOffset);
            material.SetVector(SizeID, new Vector4(rectTF.rect.width, rectTF.rect.height, 0f, 0f));
            material.SetVector(RectSizeID, new Vector4(rectTF.rect.width + rectOffset * 2f, rectTF.rect.height + rectOffset * 2f, 0f, 0f));
        }
    }

}