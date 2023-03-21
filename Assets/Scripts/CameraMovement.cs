using Player;
using UnityEngine;
using Zenject;

internal class CameraMovement : MonoBehaviour
{
    private float _distanceToPlayer;
    private Transform _playerTransform;

    [Inject]
    private void OnConstruct(PlayerMovement playerMovement)
    {
        _playerTransform = playerMovement.transform;
        _distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
    }

    private void Update()
    {
        Vector3 distanceToPlayer = _playerTransform.position - Vector3.forward * _distanceToPlayer;
        transform.position = new Vector3(distanceToPlayer.x, transform.position.y, distanceToPlayer.z);
    }
}
