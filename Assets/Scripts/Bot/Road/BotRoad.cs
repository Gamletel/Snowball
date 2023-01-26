using UnityEngine;

public class BotRoad : MonoBehaviour
{
    [SerializeField] private float _needSnowToFill;
    private bool _isFull;
    private const int MAX_SCALE = 17;
    private float _stepOfFilling;

    private void Awake()
    {
        _stepOfFilling = _needSnowToFill / MAX_SCALE;
    }

    public void FillRoad()
    {
        if (transform.localScale.z >= MAX_SCALE)
        {
            _isFull = true;
        }

        if (!_isFull)
            transform.localScale += new Vector3(0, 0, _stepOfFilling);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out BotRollingSnowball rollingSnowball))
        {
            if (!_isFull)
            {
                if (rollingSnowball.CanUnroll())
                {
                    FillRoad();
                    rollingSnowball.Unroll();
                    rollingSnowball.GetComponent<BotMovement>().UpdateCheckpoint();
                }
            }
        }
    }
}
