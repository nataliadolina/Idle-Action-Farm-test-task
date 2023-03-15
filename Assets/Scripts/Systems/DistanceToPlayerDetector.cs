using System.Collections.Generic;
using UnityEngine;
using System;
using Utilities.Behaviours;
using Utilities.Utils;
using Player;
using Zenject;

namespace Systems
{
    internal class DistanceToPlayerDetector : MultiThreadedUpdateMonoBehaviour
    {
        public event Action<float> onDistanceToSubjectChange;

        private protected Transform _aimTransform;
        private protected Transform _thisTransform;

        private float _currentDistance;

        private bool _isDetectionAreaActive = true;

        private float CurrentDistance
        {
            get => _currentDistance;
            set
            {
                _currentDistance = value;
                onDistanceToSubjectChange?.Invoke(value);
            }
        }

#region MonoBehaviour

        private void Start()
        {
            _thisTransform = transform;
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private void UpdateDistance(Vector3 aimPosition)
        {
            if (!_isDetectionAreaActive)
            {
                return;
            }

            CurrentDistance = Vector3.Distance(aimPosition, _thisTransform.position);
        }

        private protected override void SetUpUpdateSettings()
        {
            _updateThreads = new List<InvokeRepeatingSettings> {
                new InvokeRepeatingSettings(nameof(UpdateDistance))
            };
        }

        internal void ColliderSetEnabled(bool isEnabled)
        {
            _isDetectionAreaActive = isEnabled;
        }

#region Injections

        [Inject]
        private PlayerMovement _playerMovement;

        [Inject]
        private void OnConstruct()
        {
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _playerMovement.onPlayerTransformPositionChanged += UpdateDistance;
        }

        private void ClearSubscriptions()
        {
            _playerMovement.onPlayerTransformPositionChanged -= UpdateDistance;
        }

#endregion
    }
}