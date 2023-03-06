using UnityEngine;
using Utilities.Configuration;

namespace Utilities.Utils
{
    internal class InvokeRepeatingSettings
    {
        private string _methodName;
        private float _delay;
        private float _repeatRate;

        internal InvokeRepeatingSettings(string methodName, float delay = 0f, float repeatRate = GameSettingsConfig.UPDATE_RATE)
        {
            _methodName = methodName;
            _delay = delay;
            _repeatRate = repeatRate;
        }

        internal void Invoke(MonoBehaviour monoBehaviour)
        {
            monoBehaviour.InvokeRepeating(_methodName, _delay, _repeatRate);
        }

        internal void Cancel(MonoBehaviour monoBehaviour)
        {
            monoBehaviour.CancelInvoke(_methodName);
        }
    }
}
