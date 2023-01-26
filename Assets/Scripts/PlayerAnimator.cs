using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;

    private int _haveSnowballHash = Animator.StringToHash("HaveSnowball");
    private int _ridingSnowballHash = Animator.StringToHash("RidingSnowball");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    public void SetHaveSnowball(bool haveSnowball)
    {
        _animator.SetBool(_haveSnowballHash, haveSnowball);
    }

    public void RidingSnowball(bool isRiding = false)
    {
        _animator.SetBool(_ridingSnowballHash, isRiding);
        switch (isRiding)
        {
            case true:
                _rb.useGravity = false;
                break;

            case false:
                _rb.useGravity = true;
                break;
        }

    }
}
