using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEndPoint : MonoBehaviour
{
    [SerializeField] Vector3 _endPos;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + _endPos, .5f);
    }
}
