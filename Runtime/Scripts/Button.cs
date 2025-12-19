namespace JakubWegner.UIEngine {
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    public class Button : Panel, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
        [SerializeField] private PanelVisualState idleState;
        [SerializeField] private PanelVisualState hoveredState;
        [SerializeField] private PanelVisualState pressedState;

        public UnityEvent onClick;

        private bool isHovered;
        private bool isPressed;

        private void Reset() {
            Color accent = new Color(0.46f, 0.52f, 0.98f);

            // idle
            idleState.fillColor = new Color(0.18f, 0.19f, 0.23f);
            idleState.cornerRadius = 18f;

            idleState.enableBorder = true;
            idleState.borderSize = 1.5f;
            idleState.borderColor = accent * new Color(1f, 1f, 1f, 0.35f);

            idleState.enableShadow = true;
            idleState.shadowColor = new Color(0f, 0f, 0f, 0.45f);
            idleState.shadowOffset = new Vector2(0f, -6f);

            // hovered
            hoveredState.fillColor = new Color(0.22f, 0.24f, 0.30f);
            hoveredState.cornerRadius = 18f;

            hoveredState.enableBorder = true;
            hoveredState.borderSize = 1.6f;
            hoveredState.borderColor = accent * new Color(1f, 1f, 1f, 0.55f);

            hoveredState.enableShadow = true;
            hoveredState.shadowColor = new Color(0f, 0f, 0f, 0.55f);
            hoveredState.shadowOffset = new Vector2(0f, -8f);

            // pressed
            pressedState.fillColor = new Color(0.13f, 0.14f, 0.18f);
            pressedState.cornerRadius = 18f;

            pressedState.enableBorder = true;
            pressedState.borderSize = 1.4f;
            pressedState.borderColor = accent * new Color(1f, 1f, 1f, 0.65f);

            pressedState.enableShadow = true;
            pressedState.shadowColor = new Color(0f, 0f, 0f, 0.30f);
            pressedState.shadowOffset = new Vector2(0f, -2f);

            SetState(idleState);
        }
        private void OnEnable() {
            SetState(idleState);
        }
        private void OnValidate() {
            SetState(idleState);
        }

        private void SetState(PanelVisualState state) {
            this.state = state;
            UpdateMaterial();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            isHovered = true;
            if (!isPressed)
                SetState(hoveredState);
        }

        public void OnPointerExit(PointerEventData eventData) {
            isHovered = false;
            if (!isPressed)
                SetState(idleState);
        }

        public void OnPointerDown(PointerEventData eventData) {
            isPressed = true;
            SetState(pressedState);
        }

        public void OnPointerUp(PointerEventData eventData) {
            isPressed = false;
            SetState(isHovered ? hoveredState : idleState);
        }

        public void OnPointerClick(PointerEventData eventData) {
            onClick.Invoke();
        }
    }
}

