using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Player
{
    internal sealed class PlayerCut : MonoBehaviour
    {
        internal event Action onPlayerCut;

        [SerializeField]
        private GameObject instrument;

        private void Start()
        {
            instrument.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Cut();
            }
        }

        private void Cut()
        {
            instrument.SetActive(true);
            onPlayerCut?.Invoke();
        }

        private void SetInstrumentActive()
        {
            instrument.SetActive(!instrument.activeSelf);
        }
    }
}