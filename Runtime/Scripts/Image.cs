using UnityEngine;
using UnityEngine.UI;

namespace JakubWegner.UIEngine {
    public class Image : UnityEngine.UI.Image {
        private float expand;
        public void UpdateMesh(float expand) {
            this.expand = expand;
            SetVerticesDirty();
        }
        protected override void OnPopulateMesh(VertexHelper toFill) {
            base.OnPopulateMesh(toFill);
            Debug.Log("on populate mesh");
            UIVertex v = new UIVertex();
            for (int i = 0; i < 4; i++) {
                toFill.PopulateUIVertex(ref v, i);
                v.position.x += v.position.x > 0 ? expand : -expand;
                v.position.y += v.position.y > 0 ? expand : -expand;
                toFill.SetUIVertex(v, i);
            }
        }
    }

}