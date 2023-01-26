using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Time.timeScale *= 2;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale /= 2;
        }
    }
}
