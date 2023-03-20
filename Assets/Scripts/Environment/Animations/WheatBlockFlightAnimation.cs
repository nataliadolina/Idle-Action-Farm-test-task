using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using Zenject;

namespace Environment.Animations
{
    internal sealed class WheatBlockFlightAnimation : MonoBehaviour
    {
        internal event Action onWheatBlockFlightAnimationEnd;

        [SerializeField]
        private float targetX = -4;

        internal void CreateAnimationSequenceAndPlay()
        {
            StartCoroutine(TweenCoroutine());
        }

        private IEnumerator TweenCoroutine()
        {
            Tween myTween = transform.DOMoveX(targetX, 1);
            yield return myTween.WaitForKill();
            Destroy(gameObject);
            _wheat.SendStopSellProcessEvent();
        }


#region Injections

        private Wheat _wheat;

        [Inject]
        private void OnConstruct()
        {
            _wheat = GetComponentInParent<Wheat>();
        }

#endregion
    }
}
