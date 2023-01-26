using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSnowball : MonoBehaviour
{
    [SerializeField] private float _scale;
    private TrailRenderer _snowTrail;
    private PlayerAnimator _animator;
    private Snowball _snowball;
    private Rigidbody _rb;
    private const float MAX_SCALE = 2f;
    private const int MIN_SCALE = 0;
    private const float ACTIVATION_SCALE = 0.5f;
    private bool _canRoll;
    

    private void Awake()
    {
        _snowTrail = GetComponentInChildren<TrailRenderer>();
        _snowball = GetComponentInChildren<Snowball>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        if (_snowball.transform.localScale.x >= ACTIVATION_SCALE)
        {
            _snowball.gameObject.SetActive(true);
            _animator.SetHaveSnowball(true);
        }
        else
        {
            _snowball.gameObject.SetActive(false);
            _animator.SetHaveSnowball(false);
        }

        if (_rb.velocity == Vector3.zero)
            return;

        Rolling();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canRoll = true;
            _snowTrail.emitting = true;
            Debug.Log("Катаю!");
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
            Debug.Log("Не катаю!");
        }
    }

    /// <summary>
    /// Анролл, скорость которого зависит от скорости передвижения
    /// </summary>
    private void Unroll()
    {
        var _curScale = _scale * _rb.velocity.magnitude * 10;
        _snowball.transform.Rotate(Vector3.forward, _rb.velocity.magnitude);
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

        _snowball.transform.Rotate(Vector3.forward, _rb.velocity.magnitude);


        if (_snowball.transform.localScale.x >= MAX_SCALE)
            return;

        var _curScale = _scale * _rb.velocity.magnitude;
        _snowball.transform.localScale += new Vector3(_curScale, _curScale, _curScale);
        _snowball.Rolling(_curScale);
    }

    public bool CanUnroll()
    {
        return _rb.velocity != Vector3.zero && _snowball.gameObject.activeInHierarchy == true;
    }
}
