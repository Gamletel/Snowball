using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotAnimator : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private Snowball _snowball;

    private int _speedHash = Animator.StringToHash("Speed");
    private int _haveSnowballHash = Animator.StringToHash("HaveSnowball");

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _snowball = GetComponentInChildren<Snowball>();
    }

    private void Update()
    {
        _animator.SetFloat(_speedHash, _agent.velocity.magnitude);
    }

    public void SetHaveSnowball(bool haveSnowball)
    {
        _animator.SetBool(_haveSnowballHash, haveSnowball);
    }
}
