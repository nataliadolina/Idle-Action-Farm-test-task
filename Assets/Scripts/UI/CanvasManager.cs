using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Utilities.Configuration;

namespace UI
{
    internal sealed class CanvasManager : MonoBehaviour
    {
        private CanvasScaler _canvasScaler;

        [Inject]
        private CanvasSettingsConfig _canvasSettingsConfig;

        [Inject]
        private void OnConstruct()
        {
            _canvasScaler = GetComponent<CanvasScaler>();
            _canvasScaler.referenceResolution = _canvasSettingsConfig.CanvasReferenceResolution;
        }
    }
}
