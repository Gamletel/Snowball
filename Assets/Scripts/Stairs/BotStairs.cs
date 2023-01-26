using UnityEngine;
using UnityEngine.AI;

public class BotStairs : MonoBehaviour
{
    [SerializeField] private GameObject[] _steps;
    private BotStairsCollider _stairsFillCollider;
    private const float SCALE = 0.2f;
    private int _curStep = 0;
    private int _checkPointID;
    private NavMeshObstacle _obstacle;

    private void Awake()
    {
        _stairsFillCollider = GetComponentInChildren<BotStairsCollider>();
        _stairsFillCollider.transform.position = _steps[0].transform.position;
        foreach (var step in _steps)
        {
            step.SetActive(false);
        }
        _obstacle = GetComponentInChildren<NavMeshObstacle>();
    }

    public void AddStep(BotRollingSnowball botRollingSnowball)
    {
        if (botRollingSnowball.CanUnroll())
        {
            botRollingSnowball.FixedUnroll(SCALE);
            _steps[_curStep].SetActive(true);

            if (_curStep == _steps.Length - 1)
            {
                _obstacle.enabled = false;
                botRollingSnowball.FixedUnroll(SCALE);
                _steps[_curStep].SetActive(true);
                botRollingSnowball.GetComponent<BotMovement>().GoToNextCheckpoint();
                enabled = false;
                return;
            }

            _curStep++;
            _stairsFillCollider.transform.position = _steps[_curStep].transform.position;
            botRollingSnowball.GetComponent<BotMovement>().UpdateCheckpoint();
        }
        else
            botRollingSnowball.GetComponent<BotMovement>().GoToPrevCheckpoint();
    }
}
