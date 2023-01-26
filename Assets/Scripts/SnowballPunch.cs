using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class SnowballPunch : MonoBehaviour
{
    private const int FORCE = 7;
    private const int COOLDOWN = 1;
    private bool _isBot;
    private bool _canBePunched = true;
    private Rigidbody _rb;

    //For Player
    private Snowball _snowball;
    private PlayerMovement _playerMovement;
    private RollingSnowball _rollingSnowball;

    //For Bots
    private BotSnowball _botSnowball;
    private BotRollingSnowball _botRollingSnowball;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (TryGetComponent(out NavMeshAgent agent))
        {
            _agent = agent;
            _botSnowball = GetComponentInChildren<BotSnowball>();
            _botRollingSnowball = GetComponent<BotRollingSnowball>();
            _isBot = true;
        }
        else
        {
            _snowball = GetComponentInChildren<Snowball>();
            _playerMovement = GetComponent<PlayerMovement>();
            _rollingSnowball = GetComponent<RollingSnowball>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Snowball snowball))
        {
            _snowball = snowball;
            Punched();
        }

        if (other.TryGetComponent(out BotSnowball botSnowball))
        {
            Punched(botSnowball);
        }
    }

    private void Punched(BotSnowball botSnowball = null)
    {
        if (_canBePunched)
        {
            if (_isBot && botSnowball == null && _snowball.transform.localScale.z > _botSnowball.transform.localScale.z
                || _isBot && botSnowball != null && botSnowball.transform.localScale.z > _botSnowball.transform.localScale.z)
            {
                StartCoroutine(DisableAgent());
                _botSnowball.ResetSnowball();
            }

            if (!_isBot && botSnowball.transform.localScale.z > _snowball.transform.localScale.z)
            {
                StartCoroutine(DisablePlayer());
                _snowball.ResetSnowball();
            }

            var direction = transform.TransformDirection(Vector3.back) + Vector3.up * 0.5f;
            _rb.AddForce(direction * FORCE, ForceMode.Impulse);
        }

        StartCoroutine(PunchCooldown());

        Debug.Log($"{name}: Ударил!");
    }

    private IEnumerator PunchCooldown()
    {
        _canBePunched = false;
        yield return new WaitForSeconds(COOLDOWN);
        _canBePunched = true;
    }

    private IEnumerator DisableAgent()
    {
        _botRollingSnowball.enabled = false;
        _agent.enabled = false;

        yield return new WaitForSeconds(COOLDOWN);

        _botRollingSnowball.enabled = true;
        _agent.enabled = true;
    }

    private IEnumerator DisablePlayer()
    {
        _rollingSnowball.enabled = false;
        _playerMovement.LockMovement();

        yield return new WaitForSeconds(COOLDOWN);

        _rollingSnowball.enabled = true;
        _playerMovement.UnlockMovement();

    }
}
