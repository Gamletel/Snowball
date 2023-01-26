using UnityEngine;

public class BotElevatorGround : MonoBehaviour
{
    private BotElevator _elevator;

    public void GetElevatorComponent(BotElevator elevator)
    {
        _elevator = elevator;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out BotMovement botMovement))
        {
            _elevator.StartElevator(botMovement);
            enabled = false;
        }
    }
}
