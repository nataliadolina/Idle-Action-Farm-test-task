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

        private Sequence _animationSequence;

        private GameObject _gameObject;

        private Transform _modelTransform;
        private Renderer _renderer;
        private Material _material;

        internal float GrowthDuration { get => growthDuration; }

#region MonoBehaviour

        private void Awake()
        {
            _modelTransform = GetComponent<Transform>();
            _renderer = gameObject.GetComponent<Renderer>();
            _material = _renderer.material;

            _gameObject = gameObject;
        }

#endregion

        private void CreateAnimationSequence()
        {
            _animationSequence = DOTween.Sequence();
            _renderer.material.color = startColor;
            _modelTransform.localScale = new Vector3(_modelTransform.localScale.x, startScaleY, _modelTransform.localScale.z);

            var scaleTween = _modelTransform.DOScaleY(endScaleY, growthDuration);
            var doColor = _material.DOColor(endColor, growthDuration);

            _animationSequence.Append(scaleTween);
            _animationSequence.Insert(0, doColor);
            _animationSequence.Pause();
        }

        internal void StartGrowth()
        {
            _gameObject.SetActive(true);
            CreateAnimationSequence();
            _animationSequence.TogglePause();
        }            

        internal void HideWheat()
        {
            _gameObject.SetActive(false);  
        }
    }
}
