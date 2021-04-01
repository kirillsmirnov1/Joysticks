using System;
using UnityEngine;
using UnityUtils;

namespace Joysticks
{
    public class Joystick : MonoBehaviour, IVector2DirectionProvider
    {
        public Vector2 Direction { get; private set; }

        [SerializeField] private JoystickHandle handle;
        [SerializeField] private float handleRadius = 30;

        private Action _onUpdate;
        private Vector3 _handleDirection;

        private void OnValidate() => this.CheckNullFields();

        private void Update() => _onUpdate?.Invoke();

        public void OnTouchStart() => _onUpdate = HandleMovement;

        private void HandleMovement()
        {
            if (Input.GetMouseButtonUp(0))
            {
                ReleaseHandle();
            }
            else
            {
                UpdateDirection();
                UpdateHandlePosition();
            }
        }

        private void UpdateDirection()
        {
            var touchPos = Input.mousePosition;
            var rawDirection = touchPos - transform.position;
            _handleDirection = Vector3.ClampMagnitude(rawDirection, handleRadius);
            Direction = _handleDirection / handleRadius;
        }

        private void UpdateHandlePosition() => handle.SetAnchoredPosition(_handleDirection);

        public void ReleaseHandle()
        {
            _onUpdate = null;
            Direction = Vector3.zero;
            handle.ResetPosition();
        }
    }
}