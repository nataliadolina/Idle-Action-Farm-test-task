using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment.Animations;
using System;

namespace Environment
{
    internal class GrowingWheat : MonoBehaviour
    {
        //bool: is wheat currently in grow process
        internal event Action<bool> onWheatIsGrowing;

        [SerializeField]
        private float growAfterCutDelayInSeconds;

        private WheatGrowthAnimator _wheatGrowthAnimator;
        private float _growthAnimationDuration;

        private WheatColliderCutReceiver _wheatColliderCutReceiver;

        private void Start()
        {
            _wheatColliderCutReceiver = GetComponentInParent<Wheat>().WheatColliderCutReveiver;

            _wheatGrowthAnimator = GetComponentInChildren<WheatGrowthAnimator>();
            _growthAnimationDuration = _wheatGrowthAnimator.GrowthDuration;
            SetSubscriptions();

            _wheatGrowthAnimator.HideWheat();
            StartCoroutine(WaitToStartGrowingAgain(0, _growthAnimationDuration));
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

        private void OnPlayerCutWheat()
        {
            _wheatGrowthAnimator.HideWheat();
            StartCoroutine(WaitToStartGrowingAgain(growAfterCutDelayInSeconds, _growthAnimationDuration));
        }

        private IEnumerator WaitToStartGrowingAgain(float waitTime, float growthDuration)
        {
            yield return new WaitForSeconds(waitTime);
            onWheatIsGrowing?.Invoke(true);
            _wheatGrowthAnimator.StartGrowth();
            yield return new WaitForSeconds(growthDuration);
            onWheatIsGrowing?.Invoke(false);
        }

#region Subscriptions

        private void SetSubscriptions()
        {
            _wheatColliderCutReceiver.onWheatCut += OnPlayerCutWheat;
        }

        private void ClearSubscriptions()
        {
            _wheatColliderCutReceiver.onWheatCut -= OnPlayerCutWheat;
        }

#endregion
    }
}
