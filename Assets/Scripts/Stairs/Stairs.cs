using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] private GameObject[] _steps;
    private StairsCollider _stairsFillCollider;
    private const float SCALE = 0.2f;
    private int _curStep = 0;

    private void Awake()
    {
        _stairsFillCollider = GetComponentInChildren<StairsCollider>();
        _stairsFillCollider.transform.position = _steps[0].transform.position;
        foreach (var step in _steps)
        {
            step.SetActive(false);
        }
    }

    public void AddStep(PlayerMovement playerMovement)
    {
        if (playerMovement.GetComponent<RollingSnowball>().CanUnroll())
        {
            playerMovement.GetComponent<RollingSnowball>().FixedUnroll(SCALE);
            _steps[_curStep].SetActive(true);
            if (_curStep >= _steps.Length - 1)
            {
                Debug.Log("Лестница заполнена!");
                playerMovement.GetComponent<RollingSnowball>().FixedUnroll(SCALE);
                _steps[_curStep].SetActive(true);
                _stairsFillCollider.gameObject.SetActive(false);
                enabled = false;
                return;
            }
                _curStep++;
            _stairsFillCollider.transform.position = _steps[_curStep].transform.position;
        }
    }
}
