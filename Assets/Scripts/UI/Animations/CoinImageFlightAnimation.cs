using UnityEngine;
using DG.Tweening;
using Zenject;
using System.Collections;
using UI;
using Environment;

namespace UI.Animations
{
    internal class CoinImageFlightAnimation : MonoBehaviour
    {
        private Vector3 _startPosition;

#region Injections

        private Transform _aimTransform;

        [Inject]
        private CoinsPool _coinsPool;
        
        [Inject]
        private void OnConstruct(CoinsCounterTransform coinsCounterTransform)
        {
            _aimTransform = coinsCounterTransform.Transform;
            _startPosition = transform.position;
        }

#endregion

        private Tween CreateAnimationSequence()
        {
            Tween myTween = transform.DOMove(_aimTransform.position, 1);
            return myTween;
        }

        private IEnumerator TweenCoroutine()
        {
            var tween = CreateAnimationSequence();
            yield return tween.WaitForKill();
            gameObject.transform.position = _startPosition;
            gameObject.SetActive(false);
            _coinsPool.SendCoinReleaseEvent();
        }

        internal void LaunchTween()
        {
            StartCoroutine(TweenCoroutine());
        }
    }
}
