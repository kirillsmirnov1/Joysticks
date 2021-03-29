using UnityEngine;

namespace Joysticks.Editor
{
    public class MovableRectTransform : MonoBehaviour
    {
        [SerializeField] private ImplicitJoystick implicitJoystick;
        [SerializeField] private Joystick joystick;
        [SerializeField] private float speed = 1f;
        
        private RectTransform _rectTransform;
        private float _width;
        private float _height;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            
            var sizeDelta = _rectTransform.sizeDelta;
            _width = sizeDelta.x / 2;
            _height = sizeDelta.y / 2;
        }
        
        private void Update()
        {
            var dir = implicitJoystick.Direction + joystick.Direction;

            var newPos = (Vector2) _rectTransform.position + dir * speed;

            _rectTransform.position = new Vector2(
                Mathf.Clamp(newPos.x, _width, Screen.width - _width),
                Mathf.Clamp(newPos.y, _height, Screen.height - _height));
        }
    }
}