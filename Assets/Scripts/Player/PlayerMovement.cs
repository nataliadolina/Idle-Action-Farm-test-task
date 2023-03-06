using UnityEngine;
using UI;

namespace Player
{
    internal class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimator _animator;
        [SerializeField]
        private PlayerDirectionInput _directionInput;

        [SerializeField]
        private Transform _playerTransform;

        [SerializeField]
        private float speed;

        private float _speedRatio = 0f;

        private void ApplyDirection(Vector2 direction)
        {
            _speedRatio = direction.magnitude;
            _animator.SetIsRunning(_speedRatio);

            if (direction != Vector2.zero)
            {
                _playerTransform.forward = new Vector3(direction.x, 0, direction.y);
            }
            _playerTransform.Translate(Vector3.forward * speed * _speedRatio * Time.deltaTime);
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