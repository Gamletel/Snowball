using UnityEngine;

public class ElevatorGround : MonoBehaviour
{
    private Elevator _elevator;

    public void GetElevatorComponent(Elevator elevator)
    {
        _elevator = elevator;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            _elevator.StartElevator(playerMovement);
            enabled = false;
        }
    }
}
