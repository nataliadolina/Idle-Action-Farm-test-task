using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Environment
{
    internal sealed class WheatColliderCutReceiver : MonoBehaviour
    {
        internal event Action onWheatCut;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Sickle"))
            {
                onWheatCut?.Invoke();
            }
        }
    }
}
