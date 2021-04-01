using UnityEngine;
using UnityUtils.Variables.Binding;

namespace Joysticks
{
    [RequireComponent(typeof(IVector2DirectionProvider))]
    public class Vector2DirectionBinding : XVariableBinding<Vector2>
    {
        private IVector2DirectionProvider _directionProvider;

        protected override void Awake()
        {
            _directionProvider = GetComponent<IVector2DirectionProvider>();
            base.Awake();
        }

        private void Update() => BindValue();

        protected override void BindValue() => variable.Value = _directionProvider.Direction;
    }
}