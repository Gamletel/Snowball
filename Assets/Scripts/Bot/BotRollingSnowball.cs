using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class BotRollingSnowball : MonoBehaviour
{
    [HideInInspector] public float MaxScale = MAX_SCALE;
    [SerializeField] private float _scale;
    private bool _snowballIsActive;
    private TrailRenderer _snowTrail;
    private BotSnowball _snowball;
    private BotMovement _movement;
    private NavMeshAgent _agent;

    private const float MAX_SCALE = 2f;
    private const int MIN_SCALE = 0;
    private const float ACTIVATION_SCALE = 0.5f;
    private bool _canRoll;
    public bool OnTheGround;

    private BotAnimator _animator;

    private void Awake()
    {
        _snowTrail = GetComponentInChildren<TrailRenderer>();
        _snowball = GetComponentInChildren<BotSnowball>();
        _movement = GetComponent<BotMovement>();
        _animator = GetComponent<BotAnimator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (_snowball.transform.localScale.x >= ACTIVATION_SCALE  && !_snowballIsActive)
        {
            _snowball.gameObject.SetActive(true);
            _animator.SetHaveSnowball(true);
            _snowTrail.emitting = true;
            _snowballIsActive = true;
        }
        else if(_snowball.transform.localScale.x <= ACTIVATION_SCALE && _snowballIsActive)
        {
            _snowball.gameObject.SetActive(false);
            _animator.SetHaveSnowball(false);
            _snowTrail.emitting = false;
            _snowballIsActive = false;
        }

        if (_agent.velocity == Vector3.zero)
            return;

        Rolling();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canRoll = true;
            _snowTrail.emitting = true;
            OnTheGround= true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Road road))
        {
            if (CanUnroll() && !road.IsFull)
            {
                road.FillRoad();
                Unroll();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canRoll = false;
            _snowTrail.emitting = false;
            OnTheGround = false;
            Debug.Log("Не катаю!");
        }
    }

    /// <summary>
    /// Анролл, скорость которого зависит от скорости передвижения игрока
    /// </summary>
    public void Unroll()
    {
        var _curScale = _scale * _agent.velocity.magnitude * 10;
        _snowball.transform.Rotate(Vector3.forward, _agent.velocity.magnitude);
        _snowball.transform.localScale -= new Vector3(_curScale, _curScale, _curScale);
        _snowball.UnRolling(_curScale);
    }

    /// <summary>
    /// Анролл, имеющий фиксированное значение
    /// </summary>
    /// <param name="scale"></param>
    public void FixedUnroll(float scale)
    {
        _snowball.transform.localScale -= new Vector3(scale, scale, scale);
        _snowball.UnRolling(scale);
    }

    private void Rolling()
    {
        if (!_canRoll)
            return;

        _snowball.transform.Rotate(Vector3.forward, _agent.velocity.magnitude);

        if (_snowball.transform.localScale.x >= MAX_SCALE)
            return;

        var curScale = _scale * _agent.velocity.magnitude;
        _snowball.Rolling(curScale);
    }

    public bool CanUnroll()
    {
        return _snowball.gameObject.activeInHierarchy == true;
    }
}
