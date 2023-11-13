using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class PlayerMovement : MonoBehaviour
{
    public SplineFollower splineFollower;
    public BikeController bikeController;
    private float bikeSpeed = 0.0f;
    private int bikeRPM = 0;
    private int bikeHeartRate = 0;

    private float moveSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        ReadBikeData();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    // This function pulls the data from the bike controller class
    public void ReadBikeData()
    {
        bikeSpeed = bikeController.speed;
        bikeRPM = bikeController.RPM;
        bikeHeartRate = bikeController.heartRate;
    }

    // This function moves the player along the spline.
    public void MovePlayer()
    {
        splineFollower.followSpeed = bikeSpeed * moveSpeed;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
