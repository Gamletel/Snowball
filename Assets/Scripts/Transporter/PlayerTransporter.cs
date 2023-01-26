using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerTransporter : MonoBehaviour
{
    [SerializeField] private GameObject _catchPos;
    [SerializeField] private bool _isPlayerSitting;
    private bool _isCatched;

    private PlayerMovement _playerMovement;

    //Animator
    private Animator _animator;
    private int isCatchedHash = Animator.StringToHash("isCatched");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement) && !_isCatched)
        {
            _playerMovement = playerMovement;
            _playerMovement.LockMovement();
            if (_isPlayerSitting)
                _playerMovement.GetComponent<Animator>().SetBool("Sitting", true);

            _animator.SetTrigger(isCatchedHash);
            _isCatched = true;
        }
    }

    private void Update()
    {
        if (_playerMovement == null)
            return;

        _playerMovement.transform.position = _catchPos.transform.position;
    }

    public void LeavePlayer()
    {
        if (_isPlayerSitting)
            _playerMovement.GetComponent<Animator>().SetBool("Sitting", false);

        _playerMovement.UnlockMovement();
        _playerMovement = null;
    }
}
