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

        private float _maxKnobDistanceFromCenter = 48;
        private Vector2 _joystickStartPosition;

        private Vector2 _joystickUpdatedPosition;

#region MonoBehaviour

        private void Start()
        {
            _joystickStartPosition = knobTransform.position;
            _joystickUpdatedPosition = _joystickStartPosition;
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
        }

        private void SetJoystickPosition()
        {
            Vector2 touchPosition = Input.mousePosition;
            joystickTransform.position = touchPosition;
            _joystickUpdatedPosition = touchPosition;
        }

        private void SetKnobPosition()
        {
            Vector2 touchPosition = Input.mousePosition;
            Vector3 knobPosition = GetKnobPositionByDirection(touchPosition - _joystickUpdatedPosition, Vector3.Distance(touchPosition, _joystickUpdatedPosition));
            UpdateDirection(knobPosition);
        }

        private void ResetJoystick()
        {
            UpdatePlayerDirection(Vector2.zero);
            joystickGameObject.SetActive(false);
        }

        private void UpdateDirection(Vector3 knobPosition)
        {
            knobTransform.position = knobPosition;
            Vector2 direction = (new Vector2(knobPosition.x, knobPosition.y) - _joystickUpdatedPosition) / _maxKnobDistanceFromCenter;
            UpdatePlayerDirection(direction);
        }
        

#region Subscriptions

        private void SetSubscriptions()
        {
            _touchInputSystem.onIsUserTouchingScreenSetTrue += StartUpdateJoyctickDirection;
            _touchInputSystem.onIsUserTouchingScreenSetFalse += ResetJoystick;
            _touchInputSystem.onUserTouchingScreenStay += SetKnobPosition;
        }

        private void ClearSubscriptions()
        {
            _touchInputSystem.onIsUserTouchingScreenSetTrue -= StartUpdateJoyctickDirection;
            _touchInputSystem.onIsUserTouchingScreenSetFalse -= ResetJoystick;
            _touchInputSystem.onUserTouchingScreenStay -= SetKnobPosition;
        }

#endregion
    }
}
