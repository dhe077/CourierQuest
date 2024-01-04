using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class Calibration : MonoBehaviour
{
    public XROrigin origin;
    public Transform target;

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
        origin.MoveCameraToWorldLocation(target.position);
        origin.MatchOriginUpCameraForward(target.up, target.forward);

        Debug.Log("Calibrated!");
    }
}
