using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private float _needSnowToFill;
    [SerializeField] private GameObject _wall;
    [HideInInspector] public bool IsFull;
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
            IsFull = true;
            _wall.GetComponent<Collider>().enabled = false;
        }

        if (!IsFull)
            transform.localScale += new Vector3(0, 0, _stepOfFilling);
    }
}
