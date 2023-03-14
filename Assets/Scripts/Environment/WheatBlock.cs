using UnityEngine;
using Zenject;
using System;

namespace Environment
{
    internal sealed class WheatBlock : MonoBehaviour
    {
        internal event Action<Transform, Rigidbody> onAddWheatBlockToStack;

        [SerializeField]
        private WheatColliderCutReceiver wheatColliderCutReceiver;

        [SerializeField]
        private float pushOnStartForce;

        private Rigidbody _rigidBody;
        private GameObject _gameObject;
        private Collider _collider;

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        [Inject]
        private void OnConstruct()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            _gameObject = gameObject;
            _gameObject.SetActive(false);

            SetSubscriptions();
        }

        private void Appear()
        {
            _gameObject.SetActive(true);
            _rigidBody.AddForce(transform.forward * pushOnStartForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ground"))
            {
                _collider.isTrigger = true;
                _rigidBody.isKinematic = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                onAddWheatBlockToStack?.Invoke(transform, _rigidBody);
            }
        }

#region Subscriptions

        private void SetSubscriptions()
        {
            wheatColliderCutReceiver.onWheatCut += Appear;
        }

        private void ClearSubscriptions()
        {
            wheatColliderCutReceiver.onWheatCut -= Appear;
        }

#endregion
    }
}
