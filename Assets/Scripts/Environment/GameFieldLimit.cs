using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    internal sealed class GameFieldLimit : MonoBehaviour
    {
        private Vector3 _playerCollisionPosition;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Triggered with player");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                _playerCollisionPosition = collision.transform.position;
                Debug.Log("Collided with player");
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.transform.position = _playerCollisionPosition;
                Debug.Log("Collided with player");
            }
        }
    }
}
