using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class SnowTrail : MonoBehaviour
{
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
           // _trailRenderer.enabled = true;
    }
    private void OnCollisionExit(Collision collision)
    {
       // if (collision.gameObject.CompareTag("Ground"))
         //   _trailRenderer.enabled = false;
    }
}
