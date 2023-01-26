using UnityEngine;

public class StairsCollider : MonoBehaviour
{
    [SerializeField] private Stairs _stairs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            _stairs.AddStep(playerMovement);
        }
    }
}
