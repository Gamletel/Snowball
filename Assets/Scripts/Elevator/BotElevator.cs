using UnityEngine;
using UnityEngine.AI;

public class BotElevator : MonoBehaviour
{
    [SerializeField] private Vector3 _endPos;
    private const int SPEED = 4;
    private bool _isActive;
    private BotMovement _botMovement;
    private BotElevatorGround _elevatorGround;

    private void Awake()
    {
        _elevatorGround = GetComponentInChildren<BotElevatorGround>();
        _endPos += transform.position;
    }

    private void Start()
    {
        _elevatorGround.GetElevatorComponent(GetComponent<BotElevator>());
    }

    private void Update()
    {
        if (_isActive && enabled)
        {
            Move();
        }

        if (enabled && transform.position == _endPos)
        {
            LeaveBot();
        }
    }

    public void StartElevator(BotMovement botMovement)
    {
        if (!_isActive)
        {
            _isActive = true;
            _botMovement = botMovement;
            _botMovement.LockMovement();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endPos, SPEED * Time.fixedDeltaTime);
        _botMovement.gameObject.transform.position = _elevatorGround.transform.position + Vector3.up * 0.1f;
    }

    private void LeaveBot()
    {
        _botMovement.transform.position = _elevatorGround.transform.position + new Vector3(0, .1f, 1.5f);
        _botMovement.GetComponent<NavMeshAgent>().enabled = true;
        _botMovement.GoToNextCheckpoint();
        enabled = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _endPos + transform.position + Vector3.up * 0.75f);
        Gizmos.DrawCube(_endPos + transform.position + Vector3.up * 0.75f, Vector3.one);
    }
#endif
}
