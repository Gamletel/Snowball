using UnityEditor;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Vector3 _endPos;
    private const int SPEED = 4;
    private bool _isActive;
    private PlayerMovement _playerMovement;
    private ElevatorGround _elevatorGround;

    private void Awake()
    {
        _elevatorGround = GetComponentInChildren<ElevatorGround>();
        _endPos += transform.position;
    }

    private void Start()
    {
        _elevatorGround.GetElevatorComponent(GetComponent<Elevator>());
    }

    private void Update()
    {
        if (_isActive && enabled)
        {
            Move();
        }

        if (enabled && transform.position == _endPos)
        {
            LeavePlayer();
        }
    }

    public void StartElevator(PlayerMovement playerMovement)
    {
        if (!_isActive)
        {
            _isActive = true;
            _playerMovement = playerMovement;
            _playerMovement.GetComponent<Rigidbody>().isKinematic = true;
            _playerMovement.LockMovement();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endPos, SPEED * Time.deltaTime);
        _playerMovement.gameObject.transform.position = _elevatorGround.transform.position + new Vector3(0, .1f, 0);
    }

    private void LeavePlayer()
    {
        _playerMovement.transform.position = _elevatorGround.transform.position + new Vector3(0, .1f, 1.5f);
        _playerMovement.GetComponent<Rigidbody>().isKinematic = false;
        _playerMovement.UnlockMovement();
        _playerMovement = null;
        Debug.Log("Приехал");
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
