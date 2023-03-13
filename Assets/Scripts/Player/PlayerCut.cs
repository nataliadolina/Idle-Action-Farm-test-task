using UnityEngine;
using Player.Enums;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Cut();
            }
        }

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

#region Subscriptions

        private void SetSubscriptions()
        {
            playerAnimator.onCutAnimationStartedPlaying += SetInstrumentActive;
            playerAnimator.onCutAnimationStoppedPlaying += SetInstrumentInactive;
        }

        private void ClearSubscriptions()
        {
            playerAnimator.onCutAnimationStartedPlaying -= SetInstrumentActive;
            playerAnimator.onCutAnimationStoppedPlaying -= SetInstrumentInactive;
        }

#endregion

    }
}