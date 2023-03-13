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
            if (collision.collider.CompareTag("Player"))
            {
                Destroy(_gameObject);
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
