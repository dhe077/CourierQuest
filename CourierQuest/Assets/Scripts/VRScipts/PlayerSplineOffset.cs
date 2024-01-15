using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerSplineOffset : MonoBehaviour
{
    private SplineFollower playerSplineFollower;

    public Vector3 newRotationOffset;

    void Start()
    {
        playerSplineFollower = GetComponent<PlayerViewManager>().GetPlayerView().GetComponent<SplineFollower>();
    }

    public void SetSplineRotationOffset()
    {
        Debug.Log("Set the spline offset!");
        playerSplineFollower.motion.rotationOffset = newRotationOffset;
    }

    public void ResetSplineRotationOffset()
    {
        Debug.Log("Reset the spline offset!");
        playerSplineFollower.motion.rotationOffset = new Vector3(0, 0, 0);
    }


}
