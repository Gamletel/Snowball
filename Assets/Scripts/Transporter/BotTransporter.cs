using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotTransporter : MonoBehaviour
{
    [SerializeField] private GameObject _catchPos;
    [SerializeField] private bool _isBotSitting;
    private bool _isCatched;

    private BotMovement _botMovement;

    //Animator
    private Animator _animator;
    private int isCatchedHash = Animator.StringToHash("isCatched");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BotMovement botMovement) && !_isCatched)
        {
            _botMovement = botMovement;
            _botMovement.GoToNextCheckpoint();
            _botMovement.LockMovement();

            if (_isBotSitting)
                _botMovement.GetComponent<Animator>().SetBool("Sitting", true);
            
            _animator.SetTrigger(isCatchedHash);
            _isCatched = true;
        }
    }

    private void Update()
    {
        if (_botMovement == null)
            return;

        _botMovement.transform.position = _catchPos.transform.position;
    }

    public void LeavePlayer()
    {
        if (_isBotSitting)
            _botMovement.GetComponent<Animator>().SetBool("Sitting", false);

        _botMovement.GetComponent<NavMeshAgent>().enabled = true;
        _botMovement.RandomizeMovePoint();
        _botMovement = null;
    }
}
