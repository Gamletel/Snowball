using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(RollingSnowball))]
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Snowball Snowball;
    [SerializeField] private DynamicJoystick _moveJoystick;
    [SerializeField] private float _speed;
    private float _startSpeed;
    private TrailRenderer _snowTrail;
    private Rigidbody _rb;
    private Animator _animator;
    private bool _canMove = true;

    private int _speedHash = Animator.StringToHash("Speed");

    private void Awake()
    {
        _startSpeed = _speed;
        _snowTrail = GetComponentInChildren<TrailRenderer>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Snowball = GetComponentInChildren<Snowball>();
    }

    private void Start()
    {
        Finish.playerFinish += OnFinish;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!_canMove)
            return;

        if (_moveJoystick.Direction != Vector2.zero)
        {
            _rb.velocity = new Vector3(_moveJoystick.Direction.x * _speed, -1f, _moveJoystick.Direction.y * _speed);
            transform.rotation = Quaternion.LookRotation(new Vector3(_moveJoystick.Direction.x, 0f, _moveJoystick.Direction.y));
        }

        _animator.SetFloat(_speedHash, _rb.velocity.magnitude);
    }

    /// <summary>
    /// Increase Player's speed
    /// </summary>
    /// <param name="speed">Speed multiplier</param>
    public void IncreaseSpeed(float speed)
    {
        _speed *= speed;
        Debug.Log("Скорость увеличена!");
    }

    /// <summary>
    /// Decreas Player's speed
    /// </summary>
    /// <param name="speed">Speed multiplier</param>
    public void DecreaseSpeed(float speed)
    {
        _speed /= speed;
    }

    public void UnlockMovement()
    {
        if (Snowball.GetComponent<MeshRenderer>().enabled == false)
            Snowball.GetComponent<MeshRenderer>().enabled = true;

        _snowTrail.emitting = true;
        _moveJoystick.DeadZone = 0;
        _canMove = true;
        _rb.velocity = Vector3.zero;
        _animator.SetFloat(_speedHash, _rb.velocity.magnitude);

        if (Snowball.GetComponent<MeshRenderer>().enabled == false)
            Snowball.GetComponent<MeshRenderer>().enabled = true;

        Debug.Log("Могу ходить!");
    }

    public void LockMovement(bool hideSnowball = true)
    {
        _snowTrail.emitting = false;
        _canMove = false;
        _moveJoystick.DeadZone = 100;
        _rb.velocity = Vector3.zero;
        _animator.SetFloat(_speedHash, 0);

        if (hideSnowball)
            Snowball.GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Не могу ходить!");
    }

    public void FixedMovement()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
    }

    public void UnlockFixedMovement()
    {
        _rb.constraints = RigidbodyConstraints.None;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnFinish()
    {
        _canMove = false;
        _moveJoystick.enabled = false;
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
        _animator.SetFloat(_speedHash, 1);
    }

    public void ChangeSpeed(int speed = 3)
    {
        _speed = speed;
    }
}
