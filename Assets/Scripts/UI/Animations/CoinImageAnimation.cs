using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace UI.Animations
{
    internal class CoinImageAnimation : MonoBehaviour
    {
        [SerializeField]
        private float tweenDuration;

        private Vector3 _endScale;
        private Vector3 _startScale;

        private Tween _tween;
        private bool _isCoroutineLaunched = false;

        private void Start()
        {
            _startScale = transform.localScale;
            _endScale = _startScale * 1.2f;
        }

        private Tween CreateAnimationSequence()
        {
            Tween myTween = transform.DOScale(_endScale, tweenDuration).SetEase(Ease.InOutQuad).SetLoops(2, LoopType.Yoyo);
            return myTween;
        }

        internal void LaunchAnimation()
        {
            if (!_isCoroutineLaunched)
            {
                _tween = CreateAnimationSequence();
                StartCoroutine(LaunchAnimationCoroutine());
            }
        }

        private IEnumerator LaunchAnimationCoroutine()
        {
            _isCoroutineLaunched = true;
            yield return _tween.WaitForKill();
            transform.localScale = _startScale;
            _isCoroutineLaunched = false;
        }
    }
}