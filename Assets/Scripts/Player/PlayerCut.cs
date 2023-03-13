using UnityEngine;
using Zenject;
using Player.Enums;
using UI.Interfaces;

namespace Player
{
    internal sealed class PlayerCut : MonoBehaviour
    {
        [SerializeField]
        private GameObject instrument;
        [SerializeField]
        private PlayerAnimator playerAnimator;
        [SerializeField]
        private PlayerStateHandler playerStateHandler;

#region MonoBehaviour

        private void Start()
        {
            instrument.SetActive(false);
            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private void Cut()
        {
            if (playerStateHandler.CurrentState != PlayerStates.Idle & playerStateHandler.CurrentState != PlayerStates.Moving)
            {
                return;
            }

            playerStateHandler.CurrentState = PlayerStates.Cutting;
        }

        private void SetInstrumentActive()
        {
            instrument.SetActive(true);
        }

        private void SetInstrumentInactive()
        {
            instrument.SetActive(false);
        }

#region Injections

        [Inject]
        private ICutInput _cutInput;

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            playerAnimator.onCutAnimationStartedPlaying += SetInstrumentActive;
            playerAnimator.onCutAnimationStoppedPlaying += SetInstrumentInactive;
            _cutInput.onCutButtonPressed += Cut;

        }

        private void ClearSubscriptions()
        {
            playerAnimator.onCutAnimationStartedPlaying -= SetInstrumentActive;
            playerAnimator.onCutAnimationStoppedPlaying -= SetInstrumentInactive;
            _cutInput.onCutButtonPressed -= Cut;
        }

#endregion

    }
}