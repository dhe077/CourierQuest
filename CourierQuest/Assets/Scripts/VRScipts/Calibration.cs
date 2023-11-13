using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Calibration : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Recenter();
        }
    }

    public void Recenter()
    {

    }
}
