using UnityEngine;
using Zenject;
using System;
using DI;
using System.Collections;

namespace Environment
{
    internal sealed class WheatBlock : MonoBehaviour
    {
        [SerializeField]
        private float pushOnStartForce;
        [SerializeField]
        private Vector3 startLocalPosition;

        private Rigidbody _rigidBody;
        private Collider _collider;
        private Transform _parentTransform;
        private WheatColliderCutReceiver _wheatColliderCutReceiver;

        private bool _hasCloned = false;
        private Wheat _wheat;

#region MonoBehaviour

        private void OnDestroy()
        {
            ClearSubscriptions();
        }

#endregion

        [Inject]
        private void OnConstruct()
        {
            _wheat = GetComponentInParent<Wheat>();
            _wheatColliderCutReceiver = _wheat.WheatColliderCutReveiver;

            _rigidBody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();

            _collider.enabled = false;
            _rigidBody.isKinematic = true;
            _hasCloned = false;

            _parentTransform = transform.parent;
            
            SetSubscriptions();

            gameObject.SetActive(false);
        }

        private void Appear()
        {
            _collider.enabled = true;

            _collider.isTrigger = false;
            _rigidBody.isKinematic = false;
            transform.parent = null;

            gameObject.SetActive(true);
            _rigidBody.AddForce(-transform.forward * pushOnStartForce, ForceMode.Impulse);

            if (!_hasCloned)
            {
                InstantiateNewPrefab();
                ClearSubscriptions();
            }
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
                _wheat.SendAddToStackEvent(this, transform, _rigidBody);
            }
        }

        private void InstantiateNewPrefab()
        {
            GameObject newWheatBlock = ContainerRef.Container.InstantiatePrefab(Resources.Load<GameObject>("Prefabs/Environment/WheatBlock"), _parentTransform.position + startLocalPosition, Quaternion.identity, _parentTransform);
            newWheatBlock.SetActive(false);

            _hasCloned = true;
        }

#region Subscriptions

        private void SetSubscriptions()
        {
            _wheatColliderCutReceiver.onWheatCut += Appear;
        }

        private void ClearSubscriptions()
        {
            _wheatColliderCutReceiver.onWheatCut -= Appear;
        }

#endregion
    }
}
