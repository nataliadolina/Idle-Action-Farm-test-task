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

        private Tween _tween;
        private bool _isCoroutineLaunched = false;

        private void Start()
        {
            _endScale = transform.localScale * 1.2f;
            _tween = CreateAnimationSequence();
        }

        private Tween CreateAnimationSequence()
        {
            Tween myTween = transform.DOScale(_endScale, tweenDuration).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
            myTween.Pause();
            return myTween;
        }

        internal void LaunchAnimation()
        {
            if (!_isCoroutineLaunched)
            {
                StartCoroutine(LaunchAnimationCoroutine());
            }
        }

        private IEnumerator LaunchAnimationCoroutine()
        {
            _tween.TogglePause();
            _isCoroutineLaunched = true;
            yield return new WaitForSeconds(tweenDuration * 2);
            _tween.TogglePause();
            _isCoroutineLaunched = false;
        }
    }
}