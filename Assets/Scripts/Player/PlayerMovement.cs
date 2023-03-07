using UnityEngine;
using UI.Interfaces;
using Zenject;

namespace Player
{
    internal class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimator _animator;

        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private float speed;

        private void ApplyDirection(Vector2 direction)
        {
            float speedRatio = direction.magnitude;
            _animator.SetIsRunning(speedRatio);

            if (direction != Vector2.zero)
            {
                playerTransform.forward = new Vector3(direction.x, 0, direction.y);
            }
            playerTransform.Translate(Vector3.forward * speed * speedRatio * Time.deltaTime);
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