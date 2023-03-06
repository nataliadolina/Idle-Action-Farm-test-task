using UnityEngine;

namespace UI.Abstract
{
    internal abstract class ZoneBase2D : MonoBehaviour
    {
        internal abstract bool IsPositionInsideZone(Vector2 position);

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.matrix = Gizmos.matrix;
            DrawGizmos();
        }

        private protected abstract void DrawGizmos();

#endif
    }
}