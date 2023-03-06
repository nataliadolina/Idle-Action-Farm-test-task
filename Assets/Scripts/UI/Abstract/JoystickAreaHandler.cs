using System.Collections;
using UnityEngine;
using Systems;

namespace UI.Abstract
{
    internal abstract class JoystickAreaHandler : MonoBehaviour
    {
        [SerializeField]
        [Header("Dependencies")]
        private TouchInputSystem _touchInputSystem;

        [Space]

        [Header("Joystick")]
        [SerializeField] private GameObject joystickGameObject;
        [SerializeField] private Transform joystickTransform;
        [SerializeField] private Transform knobTransform;

        [Space]

        [Header("Joysticks zones")]
        [SerializeField] private UIRectangleZone2D activeJoystickZone;
        [SerializeField] private UICircleZone2D joystickZone;

        private float _maxKnobDistanceFromCenter;
        private Vector2 _joystickStartPosition;

        private Vector2 _joystickUpdatedPosition;

        private Coroutine _updateJoystickDirectionCoroutine;

#region MonoBehaviour

        private void Start()
        {
            _joystickStartPosition = knobTransform.position;
            _joystickUpdatedPosition = _joystickStartPosition;
            _maxKnobDistanceFromCenter = joystickZone.Radius - 20;
            SetSubscriptions();
            joystickGameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        private protected virtual void UpdatePlayerDirection(in Vector2 direction) { }

        private Vector2 GetKnobPositionByDirection(Vector2 direction, float distanceToCenter)
        {
            return _joystickUpdatedPosition + direction.normalized * Mathf.Clamp(distanceToCenter, 0, _maxKnobDistanceFromCenter);
        }

        private void StartUpdateJoyctickDirection()
        {
            joystickGameObject.SetActive(true);
            SetJoystickPosition();
            _updateJoystickDirectionCoroutine = StartCoroutine(UpdateJoyctickDirection());
        }

        private void SetJoystickPosition()
        {
            Vector2 touchPosition = Input.mousePosition;
            if (activeJoystickZone.IsPositionInsideZone(touchPosition))
            {
                joystickTransform.position = touchPosition;
                _joystickUpdatedPosition = touchPosition;
            }
        }

        private void ResetJoystick()
        {
            StopCoroutine(_updateJoystickDirectionCoroutine);

            UpdatePlayerDirection(Vector2.zero);

            joystickGameObject.SetActive(false);
        }

        private void UpdateDirection(Vector3 knobPosition)
        {
            knobTransform.position = knobPosition;
            Vector2 direction = (new Vector2(knobPosition.x, knobPosition.y) - _joystickUpdatedPosition) / _maxKnobDistanceFromCenter;
            UpdatePlayerDirection(direction);
        }

#region IEnumerators

        private IEnumerator UpdateJoyctickDirection()
        {
            while (true)
            {
                Vector2 touchPosition = Input.mousePosition;
                Vector3 knobPosition = GetKnobPositionByDirection(touchPosition - _joystickUpdatedPosition, Vector3.Distance(touchPosition, _joystickUpdatedPosition));
                UpdateDirection(knobPosition);

                yield return new WaitForEndOfFrame();
            }
        }

#endregion

#region Subscriptions

        private void SetSubscriptions()
        {
            _touchInputSystem.onIsUserTouchingScreenSetTrue += StartUpdateJoyctickDirection;
            _touchInputSystem.onIsUserTouchingScreenSetFalse += ResetJoystick;
        }

        private void ClearSubscriptions()
        {
            _touchInputSystem.onIsUserTouchingScreenSetTrue -= StartUpdateJoyctickDirection;
            _touchInputSystem.onIsUserTouchingScreenSetFalse -= ResetJoystick;
        }

#endregion
    }
}
