using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Finish : MonoBehaviour
{
    public delegate void PlayerFinish();
    public static event PlayerFinish playerFinish;

    public delegate void PlayerWin();
    public static event PlayerWin playerWin;

    [SerializeField] private Transform _finishPoint;
    [SerializeField] private CinemachineVirtualCamera _finishCam;
    [SerializeField] private CinemachineVirtualCamera _loseFinishCam;
    private const float SPEED = 20f;
    private Vector3 _snowballEndPos;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerFinish?.Invoke();
            StartCoroutine(StartFinishihg(playerMovement));
        }

        if(collision.gameObject.TryGetComponent(out BotMovement botMovement))
        {
            BotFinished(botMovement);
        }
    }

    private void BotFinished(BotMovement botMovement)
    {
        Debug.Log("Бот финишировал!");
        _loseFinishCam.Follow = botMovement.transform;
        _loseFinishCam.LookAt = botMovement.transform;
        _loseFinishCam.Priority = 15;
        botMovement.LockMovement();
        botMovement.enabled = false;
        botMovement.GetComponent<Animator>().SetTrigger("Finished");
        botMovement.GetComponent<Rigidbody>().isKinematic = true;
        botMovement.GetComponentInChildren<BotSnowball>().gameObject.SetActive(false);
        botMovement.transform.rotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(RestartCountDown());
    }

    private IEnumerator RestartCountDown(int timeTiRestart = 5)
    {
        yield return new WaitForSeconds(timeTiRestart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator StartFinishihg(PlayerMovement playerMovement)
    {
        Debug.Log("Финишировал!");

        _finishCam.Follow = playerMovement.GetComponentInChildren<Snowball>().transform;
        _finishCam.LookAt = playerMovement.GetComponentInChildren<Snowball>().transform;
        _finishCam.Priority = 15;
        playerMovement.LockMovement(false);
        playerMovement.enabled = false;

        var pos = playerMovement.Snowball.transform.localScale.z * 10;
        _snowballEndPos = _finishPoint.position + new Vector3(0, 0, pos);

        playerMovement.GetComponent<Animator>().SetFloat("Speed", 3);

        while(playerMovement.transform.position != _finishPoint.position)
        {
            playerMovement.transform.position = Vector3.MoveTowards(playerMovement.transform.position, _finishPoint.position, SPEED * Time.deltaTime);
            yield return new WaitForSeconds(.001f);
        }

        playerMovement.GetComponent<Animator>().SetFloat("Speed", 0);

        yield return new WaitForSeconds(1f);
        playerMovement.GetComponent<Animator>().SetTrigger("Kick");
        _finishCam.Priority = 15;
        _finishCam.enabled = true;

        while (playerMovement.Snowball.transform.position != _snowballEndPos)
        {

            playerMovement.Snowball.transform.position = Vector3.MoveTowards(playerMovement.Snowball.transform.position, _snowballEndPos, SPEED * Time.deltaTime);
            playerMovement.Snowball.transform.Rotate(Vector3.forward * SPEED);
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
