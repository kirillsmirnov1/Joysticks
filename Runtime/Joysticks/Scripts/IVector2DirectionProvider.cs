using UnityEngine;

namespace Joysticks
{
    public interface IVector2DirectionProvider
    {
        public Vector2 Direction { get; }
    }
}