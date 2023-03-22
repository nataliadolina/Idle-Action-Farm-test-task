using UnityEngine;
using UI.Interfaces;
using Zenject;
using Player.Enums;
using System;
using Environment;

namespace Player
{
    internal class PlayerMovement : MonoBehaviour
    {
        //Vector3: player position
        internal event Action<Vector3> onPlayerTransformPositionChanged;

        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private float speed;

        [SerializeField]
        private PlayerStateHandler playerStateHandler;

        private void ApplyDirection(Vector2 direction)
        {
            if (playerStateHandler.CurrentState == PlayerStates.Cutting)
            {
                return;
            }

            if (direction == Vector2.zero)
            {
                playerStateHandler.CurrentState = PlayerStates.Idle;
                return;
            }

            float speedRatio = direction.magnitude;
            playerTransform.forward = new Vector3(direction.x, 0, direction.y);
            Vector3 moveToPosition = transform.position + transform.forward * speed * speedRatio * Time.deltaTime;
            transform.position = _fieldLimits.ClampPlayerPosition(moveToPosition);
            playerStateHandler.CurrentState = PlayerStates.Moving;
            onPlayerTransformPositionChanged?.Invoke(playerTransform.position);
        }

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

#region Injections

        [Inject] private IPlayerDirectionInput _directionInput;

        [Inject] private FieldLimits _fieldLimits;

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