using UnityEngine;
using Zenject;

namespace Player
{
    internal class PlayerAnimator : MonoBehaviour
    {
        private readonly int StartRunningTrigger = Animator.StringToHash("Start running");
        private readonly int StopRunningTrigger = Animator.StringToHash("Stop running");
        private readonly int CutTrigger = Animator.StringToHash("Cut");

        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerCut playerCut;

        private Animator _animator;

        private bool _isRunning;
        private bool IsRunning
        {
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    _animator.SetTrigger(value ? StartRunningTrigger : StopRunningTrigger);
                }
            }
        }

#region MonoBehaviour

        private void Start()
        {
            _animator = GetComponent<Animator>();
            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private void SetIsRunning(bool isRunning)
        {
            IsRunning = isRunning;
        }

        private void Cut()
        {
            _animator.SetTrigger(CutTrigger);
        }

#region Subscriptions

        private void SetSubscriptions()
        {
            playerMovement.onPlayerMove += SetIsRunning;
            playerCut.onPlayerCut += Cut;
        }

        private void ClearSubscriptions()
        {
            playerMovement.onPlayerMove -= SetIsRunning;
            playerCut.onPlayerCut -= Cut;
        }

#endregion

    }
}
