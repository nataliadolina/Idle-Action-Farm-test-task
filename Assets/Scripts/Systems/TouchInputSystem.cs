using System.Collections.Generic;
using UnityEngine;
using Utilities.Behaviours;
using System;
using Utilities.Utils;

namespace Systems
{
    internal class TouchInputSystem : MultiThreadedUpdateMonoBehaviour
    {
        public event Action onIsUserTouchingScreenSetTrue;
        public event Action onIsUserTouchingScreenSetFalse;
        public event Action onUserTouchingScreenStay;

        private bool _isUserTouchingScreen;
        private bool IsUserTouchingScreen
        {
            set
            {
                if (value == _isUserTouchingScreen)
                {
                    return;
                }

                _isUserTouchingScreen = value;

                if (value)
                {
                    onIsUserTouchingScreenSetTrue?.Invoke();
                }
                else
                {
                    onIsUserTouchingScreenSetFalse?.Invoke();
                }
            }
        }

#region MonoBehaviour

        private void Start()
        {
            StartInternal();
        }

#endregion

        private void SetIsInputTouchDown()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            IsUserTouchingScreen = true;
        }

        private void SetIsInputTouchUp()
        {
            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }

            IsUserTouchingScreen = false;
        }

        private void OnInputTouchStay()
        {
            if (!_isUserTouchingScreen)
            {
                return;
            }

            onUserTouchingScreenStay?.Invoke();
        }

#region Overrides

        private protected override void SetUpUpdateSettings()
        {
            _updateThreads = new List<InvokeRepeatingSettings> {
            new InvokeRepeatingSettings(nameof(SetIsInputTouchDown)),
            new InvokeRepeatingSettings(nameof(SetIsInputTouchUp)),
            new InvokeRepeatingSettings(nameof(OnInputTouchStay))
            };
        }

#endregion
    }
}
