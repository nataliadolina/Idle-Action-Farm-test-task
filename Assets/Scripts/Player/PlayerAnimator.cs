using UnityEngine;
using Abstract;
using System;
using Player.Enums;

namespace Player
{
    internal class PlayerAnimator : AnimatorHandlerBase
    {
        internal event Action onCutAnimationStartedPlaying;
        internal event Action onCutAnimationStoppedPlaying;
        
        private readonly int RunningBool = Animator.StringToHash("Is running");
        private readonly int CutTrigger = Animator.StringToHash("Cut");

        private readonly int CutAnimationIndex = Animator.StringToHash("cut");

        [SerializeField] private PlayerStateHandler playerStateHandler;

        private bool _isRunning = false;
        private bool IsRunning
        {
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    _animator.SetBool(RunningBool, value);
                }
            }
        }

#region MonoBehaviour

        private protected override void StartInternal()
        {
            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private void Cut()
        {
            onCutAnimationStartedPlaying?.Invoke();
            _animator.SetTrigger(CutTrigger);
            WaitUntilAnimationStopPlaying(CutAnimationIndex);
        }

        private protected override void AnimationStoppedPlaying(int animationIndex)
        {
            if (animationIndex == CutAnimationIndex)
            {
                onCutAnimationStoppedPlaying?.Invoke();
            }
        }

        private void AdjustAnimationToState(PlayerStates playerState)
        {
            switch (playerState)
            {
                case PlayerStates.Cutting:
                    IsRunning = false;
                    Cut();
                    break;
                case PlayerStates.Moving:
                    IsRunning = true;
                    break;
                case PlayerStates.Idle:
                    IsRunning = false;
                    break;
            }
        }

#region Subscriptions

        private void SetSubscriptions()
        {
            playerStateHandler.onCurrentStateChanged += AdjustAnimationToState;
        }

        private void ClearSubscriptions()
        {
            playerStateHandler.onCurrentStateChanged -= AdjustAnimationToState;
        }

#endregion

    }
}
