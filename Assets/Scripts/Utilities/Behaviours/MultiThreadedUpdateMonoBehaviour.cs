using System.Collections.Generic;
using UnityEngine;
using Utilities.Utils;

namespace Utilities.Behaviours
{
    internal abstract class MultiThreadedUpdateMonoBehaviour : MonoBehaviour
    {
        private protected List<InvokeRepeatingSettings> _updateThreads;
        private protected abstract void SetUpUpdateSettings();

        private protected void StartInternal()
        {
            SetUpUpdateSettings();
            StartUpdate();
        }

        private protected void StartUpdate()
        {
            foreach (var updateThread in _updateThreads)
            {
                updateThread.Invoke(this);
            }
        }

        private protected void StopUpdate()
        {
            foreach (var updateThread in _updateThreads)
            {
                updateThread.Cancel(this);
            }
        }

        private void OnDestroy()
        {
            StopUpdate();
        }
    }
}
