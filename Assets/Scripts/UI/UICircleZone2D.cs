using UnityEngine;
using UI.Abstract;

namespace UI
{
    internal class UICircleZone2D : ZoneBase2D
    {
        [SerializeField] private float radius;

        private float _squareRadius;

        internal float Radius { get => radius; }

        private void Start()
        {
            _squareRadius = Mathf.Pow(radius, 2);
        }

        internal override bool IsPositionInsideZone(Vector2 position)
        {
            return Mathf.Pow(position.x - transform.position.x, 2) + Mathf.Pow(position.y - transform.position.y, 2) <= _squareRadius;
        }

#if UNITY_EDITOR

        private protected override void DrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

#endif
    }
}
