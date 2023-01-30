using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Ski : MonoBehaviour
{
    private int _highSpeed = 6;
    private int _timer = 10;
    private bool _used;
    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement playerMovement) && !_used)
        {

            _particle.gameObject.SetActive(false);
            StartCoroutine(SpeedUp(playerMovement));
            _used = true;
        }
    }

    private IEnumerator SpeedUp(PlayerMovement playerMovement)
    {
        playerMovement.ChangeSpeed(_highSpeed);
        yield return new WaitForSeconds(_timer);
        playerMovement.ChangeSpeed();
    }
}
