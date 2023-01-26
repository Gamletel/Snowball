using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CenterWalking : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement playerMovement))
        {
            _playerMovement = playerMovement;
            _playerMovement.transform.position = new Vector3(transform.position.x, _playerMovement.transform.position.y, _playerMovement.transform.position.z);
            _playerMovement.FixedMovement();
            //_playerAnimator.RidingSnowball(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(_playerMovement != null)
        {
            _playerMovement.UnlockFixedMovement();
            //_playerAnimator.RidingSnowball(false);
        }
    }
}
