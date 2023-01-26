using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BotMovement botMovement))
        {
            botMovement.LockMovement();
            LosePanel.OnPlayerLose();
        }
    }
}
