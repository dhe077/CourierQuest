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

    private float testSpeedMod = 0;

    // Update is called once per frame
    void Update()
    {
        ReadBikeData();

        TestControls();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    // This function pulls the data from the bike controller class
    public void ReadBikeData()
    {
        // bikeSpeed = bikeController.speed;
        bikeSpeed = bikeController.speed + testSpeedMod;
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

    public float GetBikeSpeed()
    {
        return bikeSpeed;
    }

    public float GetMoveSpeedMultiplier()
    {
        return moveSpeed;
    }

    public void TestControls()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            testSpeedMod -= 1;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            testSpeedMod += 1;
    }
}
