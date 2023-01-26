using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Finish : MonoBehaviour
{
    public delegate void PlayerFinish();
    public static event PlayerFinish playerFinish;

    public delegate void PlayerWin();
    public static event PlayerWin playerWin;

    [SerializeField] private Transform _finishPoint;
    [SerializeField] private CinemachineVirtualCamera _finishCam;
    private PlayerMovement _playerMovement;
    private const float SPEED = 2f;
    private Vector3 _snowballEndPos;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            _playerMovement = playerMovement;
            playerFinish?.Invoke();
            StartCoroutine(StartFinishihg());
        }
    }

    private IEnumerator StartFinishihg()
    {
        Debug.Log("Финишировал!");

        var pos = _playerMovement.Snowball.transform.localScale.z * 10;
        _snowballEndPos = _finishPoint.position + new Vector3(0, 0, pos);

        while(_playerMovement.transform.position != _finishPoint.position)
        {
            _playerMovement.transform.position = Vector3.MoveTowards(_playerMovement.transform.position, _finishPoint.position, SPEED * Time.deltaTime);
            yield return new WaitForSeconds(.001f);
        }
        _playerMovement.GetComponent<Animator>().SetFloat("Speed", 0);

        yield return new WaitForSeconds(1f);
        _playerMovement.GetComponent<Animator>().SetTrigger("Kick");
        _finishCam.Priority = 15;
        _finishCam.enabled = true;

        while (_playerMovement.Snowball.transform.position != _snowballEndPos)
        {
            
            _playerMovement.Snowball.transform.position = Vector3.MoveTowards(_playerMovement.Snowball.transform.position, _snowballEndPos, SPEED * Time.deltaTime);
            _playerMovement.Snowball.transform.Rotate(Vector3.forward * SPEED);
            yield return new WaitForSeconds(.001f);
        }

        yield return new WaitForSeconds(1f);
        playerWin?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_snowballEndPos, .5f);
    }
}
