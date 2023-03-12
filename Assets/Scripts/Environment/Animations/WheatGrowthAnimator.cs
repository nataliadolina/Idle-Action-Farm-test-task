using UnityEngine;
using DG.Tweening;

namespace Environment.Animations
{
    internal class WheatGrowthAnimator : MonoBehaviour
    {
        [SerializeField]
        private WheatColliderCutReceiver wheatColliderCutReceiver;

        [SerializeField]
        private float startScaleY;
        [SerializeField]
        private float endScaleY;

        [SerializeField]
        private Color startColor;
        [SerializeField]
        private Color endColor;

        [SerializeField]
        private float growthDuration;
        private Sequence _animationSequence;

        private GameObject _gameObject;

#region MonoBehaviour

        private void Start()
        {
            _gameObject = gameObject;
            CreateAnimationSequence();
            SetSubscriptions();
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private void CreateAnimationSequence()
        {
            _animationSequence = DOTween.Sequence();

            Transform modelTransform = GetComponent<Transform>();
            Renderer renderer = gameObject.GetComponent<Renderer>();
            Material material = renderer.material;

            renderer.material.color = startColor;

            modelTransform.localScale = new Vector3(modelTransform.localScale.x, startScaleY, modelTransform.localScale.z);

            var scaleTween = modelTransform.DOScaleY(endScaleY, growthDuration);
            var doColor = material.DOColor(endColor, growthDuration);

            _animationSequence.Append(scaleTween);
            _animationSequence.Insert(0, doColor);
            _animationSequence.Pause();

            _animationSequence.Complete();
        }

        private void CompleteSequence()
        {
            _gameObject.SetActive(true);
            _animationSequence.Complete();
        }

        private void StartGrowth()
        {
            _gameObject.SetActive(true);
            _animationSequence.Rewind();
            _animationSequence.Play();
        }

        private void HideWheat()
        {
            _gameObject.SetActive(false);
        }

#region Subscriptions

        private void SetSubscriptions()
        {
            wheatColliderCutReceiver.onWheatCut += HideWheat;
        }

        private void ClearSubscriptions()
        {
            wheatColliderCutReceiver.onWheatCut -= HideWheat;
        }

#endregion

    }
}
