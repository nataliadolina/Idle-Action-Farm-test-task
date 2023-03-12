using UnityEngine;
using UI.Interfaces;
using Zenject;
using System;

namespace Player
{
    internal class PlayerMovement : MonoBehaviour
    {
        /// <summary>
        /// bool - is player moving?
        /// </summary>
        internal event Action<bool> onPlayerMove;

        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private float speed;

        private void ApplyDirection(Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                onPlayerMove?.Invoke(false);
                return;
            }

            float speedRatio = direction.magnitude;
            playerTransform.forward = new Vector3(direction.x, 0, direction.y);
            playerTransform.Translate(Vector3.forward * speed * speedRatio * Time.deltaTime);

            onPlayerMove?.Invoke(true);
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region Dependencies

        [Inject] private IPlayerDirectionInput _directionInput;

        [Inject]
        private void OnConstruct()
        {
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _directionInput.onCharacterDirectionChanged += ApplyDirection;
        }

        private void ClearSubscriptions()
        {
            _directionInput.onCharacterDirectionChanged += ApplyDirection;
        }

#endregion
    }
}