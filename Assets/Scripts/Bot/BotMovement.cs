using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(BotRollingSnowball))]
[RequireComponent(typeof(BotAnimator))]
public class BotMovement : MonoBehaviour
{
    [SerializeField] private Checkpoint[] _checkpoints;
    public int _curCheckpointID = 0;
    public bool _haveCheckpoint;
    private TrailRenderer _snowTrail;
    private Vector3 _movePoint;
    private NavMeshAgent _agent;
    private NavMeshPath _path;
    private BotRollingSnowball _rollingSnowball;
    private BotSnowball _snowball;
    private const int SPEED = 3;

    private void Awake()
    {
        _snowTrail = GetComponentInChildren<TrailRenderer>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = SPEED;
        _path = new NavMeshPath();
        _rollingSnowball = GetComponent<BotRollingSnowball>();
        _snowball = GetComponentInChildren<BotSnowball>();
    }

    private void Start()
    {
        RandomizeMovePoint();
    }

    private void Update()
    {
        if (_snowball.transform.localScale.x >= _rollingSnowball.MaxScale && !_haveCheckpoint)
        {
            GoToNextCheckpoint();
        }

        if (transform.position == _agent.destination && _rollingSnowball.OnTheGround && !_haveCheckpoint)
        {
            RandomizeMovePoint();
        }
    }

    public void GoToNextCheckpoint()
    {
        _curCheckpointID++;
        _haveCheckpoint = true;
        _agent.SetDestination(_checkpoints[_curCheckpointID].transform.position);
        Debug.Log($"Следующая точка #{_curCheckpointID}: " + _checkpoints[_curCheckpointID].name);
    }

    private void GoToCurCheckpoint()
    {
        _haveCheckpoint = true;
        _agent.SetDestination(_checkpoints[_curCheckpointID].transform.position);
    }

    public void GoToPrevCheckpoint()
    {
        _curCheckpointID--;
        _haveCheckpoint = false;
        RandomizeMovePoint();
    }

    public void UpdateCheckpoint()
    {
        _agent.SetDestination(_checkpoints[_curCheckpointID].transform.position);
        _haveCheckpoint = true;
    }

    public void RandomizeMovePoint()
    {
        Debug.Log(name + ": Рандомизирую!");
        _haveCheckpoint = false;
        do
        {
            _movePoint = _checkpoints[_curCheckpointID].transform.position + new Vector3(
                Random.Range(-5f, 5f),
                0,
                Random.Range(-5f, 5f)
                );
            _agent.CalculatePath(_movePoint, _path);
        } while (_path.status == NavMeshPathStatus.PathPartial || _path.status == NavMeshPathStatus.PathInvalid);

        _agent.SetDestination(_movePoint);
    }

    public void LockMovement()
    {
        _snowTrail.emitting = false;
        _agent.enabled = false;
    }

    public void UnlockMovement()
    {
        if (_snowball.enabled == true)
            _snowTrail.emitting = true;

        _agent.enabled = true;
    }

    public void IncreaseSpeed(int speed)
    {
        _agent.speed = speed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (!_haveCheckpoint)
        {
            Gizmos.DrawLine(transform.position, _movePoint + Vector3.up * .5f);
        }
        else
        {
            Gizmos.DrawLine(transform.position, _checkpoints[_curCheckpointID].transform.position);
        }

    }
}
