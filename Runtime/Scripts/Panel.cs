namespace JakubWegner.UIEngine {
    using UnityEngine;

    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class Panel : MonoBehaviour {
        private bool initialized = false;

        private RectTransform rectTF;
        private Image image;

        private Material material;

        // fill
        [SerializeField] private Color fillColor = Color.white;

        // corners
        [SerializeField] private float cornerRadius = 20f;

        // border
        [SerializeField] private bool enableBorder = false;
        [SerializeField] private Color borderColor = Color.black;
        [SerializeField] private float borderSize = 5f;

        // shadow
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
            Debug.Log("Initialize");
            initialized = true;

            rectTF = GetComponent<RectTransform>();
            image = GetComponent<Image>();

            CreateMaterial();
        }
        private void CreateMaterial() {
            Debug.Log("Create material");
            material = new Material(Shader.Find("JakubWegner/UIEngine/Panel"));
            image.material = material;
        }
        public void UpdateMaterial() {
            if (!initialized)
                Initialize();
            Debug.Log("Update material");
            if (!material || material.shader != Shader.Find("JakubWegner/UIEngine/Panel"))
                CreateMaterial();

            // fill
            material.SetColor("_FillColor", fillColor);

            // corners
            material.SetFloat("_CornerRadius", cornerRadius);

            // border
            material.SetFloat("_EnableBorder", enableBorder ? 1f : 0f);
            if (enableBorder) {
                material.SetColor("_BorderColor", borderColor);
                material.SetFloat("_BorderSize", borderSize);
            }

            // shadow
            float rectOffset = 0f;
            material.SetFloat("_EnableShadow", enableShadow ? 1f : 0f);
            if (enableShadow) {
                rectOffset = Mathf.Max(Mathf.Abs(shadowOffset.x), Mathf.Abs(shadowOffset.y));
                material.SetColor("_ShadowColor", shadowColor);
                material.SetVector("_ShadowOffset", new Vector4(shadowOffset.x, shadowOffset.y));
            }

            // size
            image.UpdateMesh(rectOffset);
            material.SetVector("_Size", new Vector4(rectTF.rect.width, rectTF.rect.height, 0f, 0f));
            material.SetVector("_RectSize", new Vector4(rectTF.rect.width + rectOffset * 2f, rectTF.rect.height + rectOffset * 2f, 0f, 0f));
        }
    }

}