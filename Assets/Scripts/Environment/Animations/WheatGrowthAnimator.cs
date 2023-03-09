using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Environment.Animations
{
    internal class WheatGrowthAnimator : MonoBehaviour
    {
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

        private Tween _tween;
        private Sequence _animationSequence;

        private void Start()
        {
            _animationSequence = DOTween.Sequence();
            
            Transform modelTransform = GetComponent<Transform>();
            Renderer renderer = gameObject.GetComponent<Renderer>();
            Material material = renderer.material;

            renderer.material.color = startColor;

            modelTransform.localScale = new Vector3(modelTransform.localScale.x, startScaleY, modelTransform.localScale.z);

            var scaleTween = modelTransform.DOScaleY(endScaleY, growthDuration).Pause();
            var doColor = material.DOColor(endColor, growthDuration);

            _animationSequence.Append(scaleTween);
            _animationSequence.Insert(0, doColor);

            StartGrowth();
        }

        private void StartGrowth()
        {
            _tween.TogglePause();
        }
    }
}
