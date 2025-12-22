namespace JakubWegner.UIEngine {
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    public class Button : Panel, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
        [SerializeField] private PanelVisualState idleState;
        [SerializeField] private PanelVisualState hoveredState;
        [SerializeField] private PanelVisualState pressedState;
        [SerializeField] private PanelVisualState inactiveState;

        public UnityEvent onClick;
        public bool isActive;

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

            // inactive
            inactiveState.fillColor = new Color(0.13f, 0.14f, 0.18f);
            inactiveState.cornerRadius = 18f;

            inactiveState.enableBorder = true;
            inactiveState.borderSize = 1.4f;
            inactiveState.borderColor = accent * new Color(1f, 1f, 1f, 0.65f);

            inactiveState.enableShadow = true;
            inactiveState.shadowColor = new Color(0f, 0f, 0f, 0.30f);
            inactiveState.shadowOffset = new Vector2(0f, -2f);

            isHovered = false;
            isPressed = false;
            isActive = true;

            SetState(idleState);
        }
        private void OnEnable() {
            SetState(isActive ? idleState : inactiveState);
            UpdateMaterial();
        }
        private void OnValidate() {
            UpdateMaterial();
        }

        private void SetState(PanelVisualState state) {
            this.state = state;
            UpdateMaterial();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            isHovered = true;
            if (isActive && !isPressed)
                SetState(hoveredState);
        }

        public void OnPointerExit(PointerEventData eventData) {
            isHovered = false;
            if (isActive && !isPressed)
                SetState(idleState);
        }

        public void OnPointerDown(PointerEventData eventData) {
            isPressed = true;
            if (isActive)
                SetState(pressedState);
        }

        public void OnPointerUp(PointerEventData eventData) {
            isPressed = false;
            if (isActive)
                SetState(isHovered ? hoveredState : idleState);
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (isActive)
                onClick.Invoke();
        }

        public void SetActive(bool active) {
            isActive = active;
            if (active)
                SetState(isPressed ? pressedState : isHovered ? hoveredState : idleState);
            else
                SetState(inactiveState);
        }
    }
}

