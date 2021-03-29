using System;
using UnityEngine;
using UnityUtils;

namespace Joysticks
{
    public class ImplicitJoystick : MonoBehaviour
    {
        public Vector2 Direction => joystick.Direction;
        
        [SerializeField] private Joystick joystick;
        
        private Action _onUpdate;
        
        private RectTransform _joystickRect;
        private RectTransform _inputArea;

        private void OnValidate() => this.CheckNullFields();

        private void Awake()
        {
            DisableJoystick();
            InitFields();
        }

        private void Update() => _onUpdate?.Invoke();

        private void InitFields()
        {
            _joystickRect = joystick.GetComponent<RectTransform>();
            _inputArea = GetComponent<RectTransform>();
            _onUpdate = WaitForTouchStart;
        }

        private void WaitForTouchStart()
        {
            if (NoValidTouch()) return;
            
            EnableJoystick();

            _onUpdate = WaitForTouchEnd;
        }

        private bool NoValidTouch() => !(Input.GetMouseButtonDown(0) && TouchInImplicitRect());

        private bool TouchInImplicitRect()
        {
            var mousePos = Input.mousePosition;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _inputArea, 
                mousePos, 
                null,
                out var localMousePos);
            
            return _inputArea.rect.Contains(localMousePos);
        }

        private void WaitForTouchEnd()
        {
            if (!Input.GetMouseButtonUp(0)) return;
            
            DisableJoystick();
            
            _onUpdate = WaitForTouchStart;
        }

        private void EnableJoystick()
        {
            _joystickRect.position = Input.mousePosition;
            joystick.gameObject.SetActive(true);
            joystick.OnTouchStart();
        }

        private void DisableJoystick()
        {
            joystick.ReleaseHandle();
            joystick.gameObject.SetActive(false);
        }
    }
}