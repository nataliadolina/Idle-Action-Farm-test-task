using UnityEngine;
using Player.Enums;
using System;

namespace Player
{
    internal class PlayerStateHandler : MonoBehaviour
    {
        private static readonly PlayerStates StartState = PlayerStates.Idle;
        [SerializeField]
        private PlayerAnimator playerAnimator;

        internal event Action<PlayerStates> onCurrentStateChanged;

        private PlayerStates _currentState = StartState;
        internal PlayerStates CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState != value)
                {
                    _currentState = value;
                    onCurrentStateChanged?.Invoke(value);
                }
            }
        }

#region MonoBehaviour

        private void Start()
        {
            SetSubscriptions();
        }

#endregion

        private void ToStartState()
        {
            CurrentState = StartState;
        }

#region Subscriptions

        private void SetSubscriptions()
        {
            playerAnimator.onCutAnimationStoppedPlaying += ToStartState;
        }

        private void ClearSubscriptions()
        {
            playerAnimator.onCutAnimationStoppedPlaying -= ToStartState;
        }

#endregion

    }
}
