using System.Collections.Generic;
using UnityEngine;
using System;
using Utilities.Behaviours;
using Utilities.Utils;
using Player;

namespace Systems
{
    internal class DistanceToSubjectDetector : MultiThreadedUpdateMonoBehaviour
    {
        public event Action<float> onDistanceToSubjectChange;

        private protected Transform _aimTransform;
        private protected Transform _thisTransform;

        private float _currentDistance;

        private bool _isDetectionAreaActive = true;
        public Transform AimTransform { get => _aimTransform; }

        private float CurrentDistance
        {
            get => _currentDistance;
            set
            {
                _currentDistance = value;
                onDistanceToSubjectChange?.Invoke(value);
            }
        }

        private void Start()
        {
            _thisTransform = transform;
            _aimTransform = FindObjectOfType<PlayerMovement>().transform;
            StartInternal();
        }

        private void UpdateDistance()
        {
            if (!_isDetectionAreaActive)
            {
                return;
            }

            CurrentDistance = Vector3.Distance(_aimTransform.position, _thisTransform.position);
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
    }
}