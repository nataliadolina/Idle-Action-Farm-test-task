using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abstract
{
    internal abstract class AnimatorHandlerBase : MonoBehaviour
    {
        private protected Animator _animator;
        private readonly Dictionary<int, float> _animationIndexDurationMap = new Dictionary<int, float>();

#region MonoBehaviour

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            GetAnimationDurations();
            StartInternal();
        }

#endregion

        private protected virtual void StartInternal() { }

        private void GetAnimationDurations()
        {
            foreach (var clip in _animator.runtimeAnimatorController.animationClips)
            {
                string name = clip.name;
                float duration = clip.length;
                int index = Animator.StringToHash(name);
                _animationIndexDurationMap.Add(index, duration);
            }
        }

        private protected void WaitUntilAnimationStopPlaying(int animationIndex)
        {
            float duration = _animationIndexDurationMap[animationIndex];
            StartCoroutine(WaitCoroutine(duration, animationIndex));
        }

        private protected virtual void AnimationStoppedPlaying(int animationIndex) { }

        private IEnumerator WaitCoroutine(float duration, int animationIndex)
        {
            float currentTime = 0;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            AnimationStoppedPlaying(animationIndex);
        }

    }
}

