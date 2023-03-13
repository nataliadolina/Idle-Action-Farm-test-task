using UnityEngine;

namespace UI
{
    internal class UIRectangleZone : MonoBehaviour
    {
        [Header("Bounds transforms")]
        [SerializeField]
        private RectTransform left;
        [SerializeField]
        private RectTransform right;
        [SerializeField]
        private RectTransform down;
        [SerializeField]
        private RectTransform up;

        internal Vector2 ClampPosition(Vector2 position)
        {
            float minPositionX = left.position.x;
            float maxPositionX = right.position.x;
            float minPositionY = down.position.y;
            float maxPositionY = up.position.y;

            return new Vector2(Mathf.Clamp(position.x, minPositionX, maxPositionX), Mathf.Clamp(position.y, minPositionY, maxPositionY));
        }
    }
}