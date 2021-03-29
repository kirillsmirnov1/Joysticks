using UnityEngine;
using UnityEngine.EventSystems;
using UnityUtils;

namespace Joysticks
{
    public class JoystickHandle : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Joystick joystick;

        private void OnValidate() => this.CheckNullFields();

        public void OnPointerDown(PointerEventData eventData) => joystick.OnTouchStart();
        
        public void SetGlobalPosition(Vector2 pos) => rectTransform.position = pos;
        
        public void SetAnchoredPosition(Vector2 pos) => rectTransform.anchoredPosition = pos;
        
        public void ResetPosition() => SetAnchoredPosition(Vector3.zero);
    }
}