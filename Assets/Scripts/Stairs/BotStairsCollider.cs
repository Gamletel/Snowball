using UnityEngine;
using UnityEngine.AI;

public class BotStairsCollider : MonoBehaviour
{
    [SerializeField] private BotStairs _stairs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BotRollingSnowball botRollingSnowball))
        {
            if (botRollingSnowball.CanUnroll())
            {
                _stairs.AddStep(botRollingSnowball);
                Debug.LogWarning("Ступень заполнена!");
            }
            else
            {
                botRollingSnowball.GetComponent<BotMovement>().GoToPrevCheckpoint();
            }
        }
    }
}
