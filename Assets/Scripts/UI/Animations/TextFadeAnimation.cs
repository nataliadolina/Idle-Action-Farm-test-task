using UnityEngine;
using TMPro;
using DG.Tweening;
using Zenject;
using Player;
using System.Collections;

namespace UI.Animations
{
    internal class TextFadeAnimation : MonoBehaviour
    {
        private TMP_Text _text;
        private Tween _tween;

        private bool _isCoroutineLaunched = false;

#region MonoBehaviour

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            _text.alpha = 0f;
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private Tween CreateAnimationSequence()
        {
            Tween tween = _text.DOFade(1, 0.5f).SetLoops(2, LoopType.Yoyo);
            return tween;
        }

        private void LaunchAnimation()
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
            CreateAnimationSequence();
            yield return _tween.WaitForKill();
            _text.alpha = 0f;
            _isCoroutineLaunched = false;
        }

#region Injections

        [Inject]
        private HarvestStack _harvestStack;

        [Inject]
        private void OnConstruct()
        {
            SetSubscriptions();
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _harvestStack.onStackIsFull += LaunchAnimation;
        }

        private void ClearSubscriptions()
        {
            _harvestStack.onStackIsFull -= LaunchAnimation;
        }

#endregion

    }
}
