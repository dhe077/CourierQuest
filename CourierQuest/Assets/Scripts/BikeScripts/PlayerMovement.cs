using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class PlayerMovement : MonoBehaviour
{
    public SplineFollower splineFollower;
    public BikeController bikeController;
    [SerializeField] private float bikeSpeed = 0.0f;
    [SerializeField] private int bikeRPM = 0;
    private int bikeHeartRate = 0;

    private float moveSpeed = 10.0f;

    [SerializeField] private float testSpeedMod = 0;
    [SerializeField] private int testRPMMod = 0;

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
        // bikeRPM = bikeController.RPM;
        bikeRPM = bikeController.RPM + testRPMMod;

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

    public float GetBikeRPM()
    {
        return bikeRPM;
    }

    public void TestControls()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            testSpeedMod -= 1;
            testRPMMod -= 10;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            testSpeedMod += 1;
            testRPMMod += 10;
        }
            
    }
}
