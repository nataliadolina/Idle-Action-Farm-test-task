using UnityEngine;
using UI.Abstract;

namespace UI
{
    /// <summary>
    /// Rectangle zone used in UI
    /// </summary>
    internal class UIRectangleZone2D : ZoneBase2D
    {
        [SerializeField] private Vector2 zoneSize;

        /// <summary>
        /// point that doesn't change its position while changing zone size
        /// </summary>
        [SerializeField]
        private Vector3 zoneFixingPoint;

        internal override bool IsPositionInsideZone(Vector2 position)
        {
            return position.x <= zoneFixingPoint.x && position.x >= zoneFixingPoint.x - zoneSize.x
                && position.y >= zoneFixingPoint.y && position.y <= zoneFixingPoint.y + zoneSize.y;
        }

        private Vector3 GetZoneCenter(Vector3 fixingPoint)
        {
            return new Vector3(fixingPoint.x - zoneSize.x / 2, fixingPoint.y + zoneSize.y / 2, fixingPoint.z);
        }

#if UNITY_EDITOR

        private protected override void DrawGizmos()
        {
            Gizmos.DrawWireCube(GetZoneCenter(zoneFixingPoint), new Vector3(zoneSize.x, zoneSize.y, 1));
        }

#endif
    }
}
