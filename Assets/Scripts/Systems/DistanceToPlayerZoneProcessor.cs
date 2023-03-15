using UnityEngine;
using System;

namespace Systems
{
    internal class DistanceToPlayerZoneProcessor : MonoBehaviour
    {
        internal event Action onAimEnterZone;
        internal event Action onAimExitZone;

        [SerializeField] private float maxVisibleDistance;
        [SerializeField] private DistanceToPlayerDetector distanceToPlayerDetector;
        [Space]

        private bool _isSubjectInsideZone = false;
        internal bool IsSubjectInsideZone
        {
            get => _isSubjectInsideZone;
            set
            {
                if (_isSubjectInsideZone == value)
                {
                    return;
                }

                if (!value)
                {
                    onAimExitZone?.Invoke();
                }
                else if (value)
                {
                    onAimEnterZone?.Invoke();
                }
                _isSubjectInsideZone = value;
            }
        }

#region MonoBehaviour

        private void Start()
        {
            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private void SetIsSubjectInsideZone(float distanceToAim)
        {
            IsSubjectInsideZone = distanceToAim <= maxVisibleDistance ? true : false;
        }

#region Subscriptions

        private void SetSubscriptions()
        {
            distanceToPlayerDetector.onDistanceToSubjectChange += SetIsSubjectInsideZone;
        }

        private void ClearSubscriptions()
        {
            distanceToPlayerDetector.onDistanceToSubjectChange -= SetIsSubjectInsideZone;
        }

#endregion

#if UNITY_EDITOR

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, maxVisibleDistance);
        }

#endif
    }
}
