using UnityEngine;

namespace Player
{
    internal class PlayerAnimator : MonoBehaviour
    {
        private readonly int StartRunningTrigger = Animator.StringToHash("Start running");
        private readonly int StopRunningTrigger = Animator.StringToHash("Stop running");
        
        private Animator _animator;

        private bool _isRunning;
        private bool SpeedRatio
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

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        internal void SetIsRunning(float ratio)
        {
            SpeedRatio = ratio > 0;
        }
    }
}
