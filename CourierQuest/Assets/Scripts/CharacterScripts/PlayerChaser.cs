using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
    private GameObject playerView;
    private bool startChasing = false;
    private GameObject chaserObject;

    public bool atPlayerSide = true;

    [SerializeField] private SplineComputer chaserSplinePath;

    private float playerViewPercent = 0;
    private float playerViewFollowSpeed = 0;

    private float chaserPercent = 0;
    private float chaserFollowSpeed = 0;
    private int splineOffset = 0;

    [Header ("----Foward/Backward Movement Range----")]
    [SerializeField] private float maxDiff = 5f;
    [SerializeField] private float minDiff = 1f;
    [SerializeField] private float yOffset = 0f;
    [SerializeField] private float besidePlayerOffset = 0f;

    [Header ("----Speed Range----")]
    [SerializeField] private int maxRndSpeedIncrease = 5;
    [SerializeField] private int minRndSpeedIncrease = -5;

    [Header ("----Left/Right Movement Range----")]
    [SerializeField] private List<int> offsetValues;

    [Header ("----Timing----")]
    [SerializeField] private float changeOffsetTimeInterval = 10f;
    [SerializeField] private float reduceRangeTimeInterval = 10f;

    // Update is called once per frame
    void Update()
    {
        if (startChasing == true)
        {
            playerViewPercent = (float) playerView.GetComponent<SplineFollower>().result.percent;
            playerViewFollowSpeed = playerView.GetComponent<SplineFollower>().followSpeed;
            chaserPercent = (float) chaserObject.GetComponent<SplineFollower>().result.percent;
            chaserFollowSpeed = chaserObject.GetComponent<SplineFollower>().followSpeed;
            
            // Increase the chaser's speed until it reaches the player
            float interpolatedSpeed;
            float distanceDiff = CalculateDistanceFromPlayer();
            if (-distanceDiff > maxDiff) // ahead of player
            {
                interpolatedSpeed = Mathf.Lerp(chaserFollowSpeed, playerViewFollowSpeed * -2, Time.deltaTime);
            }
            else if (-distanceDiff < minDiff)
            {
                interpolatedSpeed = Mathf.Lerp(chaserFollowSpeed, playerViewFollowSpeed * 2, Time.deltaTime);
            }
            else
            {
                // Make random speed
                System.Random rnd = new System.Random();
                float rndSpeed = chaserFollowSpeed + rnd.Next(minRndSpeedIncrease, maxRndSpeedIncrease);

                interpolatedSpeed = Mathf.Lerp(chaserFollowSpeed, rndSpeed, Time.deltaTime);
            }
            chaserObject.GetComponent<SplineFollower>().followSpeed = interpolatedSpeed;

            // Move next to player or around the player
            if (atPlayerSide)
            {
                FollowBesidePlayer();
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    public void StartChasing(GameObject pv, GameObject chaser)
    {
        playerView = pv;

        // Spawn the object
        chaserObject = Instantiate(chaser, Vector3.zero, Quaternion.identity);
        chaserObject.name = chaser.name;
        Debug.Log("Spawned Object!");
        chaserObject.GetComponent<SplineFollower>().spline = chaserSplinePath;
        Debug.Log("Set object's spline!");

        // Begin the chase
        startChasing = true;
    }

    public void FollowBesidePlayer()
    {
        float interpolatedPosition = Mathf.Lerp(chaserObject.GetComponent<SplineFollower>().motion.offset.x, besidePlayerOffset, Time.deltaTime);
        chaserObject.GetComponent<SplineFollower>().motion.offset = new Vector2(interpolatedPosition, yOffset);
    }

    private void FollowPlayer()
    {
        float interpolatedPosition = Mathf.Lerp(chaserObject.GetComponent<SplineFollower>().motion.offset.x, splineOffset, Time.deltaTime);
        chaserObject.GetComponent<SplineFollower>().motion.offset = new Vector2(interpolatedPosition, yOffset);
    }

    public void SetAtPlayerSide(bool x)
    {
        atPlayerSide = x;
        if (x == false)
        {
            // Start InvokeRepeating functions
            StartInvokeRepeating();
        }
    }

    private float CalculateDistanceFromPlayer()
    {
        float splineDistance = chaserSplinePath.CalculateLength();
        float actualChaserDistance = chaserPercent * splineDistance;
        float actualPlayerDistance = playerViewPercent * splineDistance;

        return actualPlayerDistance - actualChaserDistance;
    }

    private void StartInvokeRepeating()
    {
        InvokeRepeating("SetSplinePosition", 10f, changeOffsetTimeInterval);
        InvokeRepeating("ReduceRange", 10f, reduceRangeTimeInterval);
    }

    private void SetSplinePosition()
    {
        System.Random rnd = new System.Random();
        splineOffset = offsetValues[rnd.Next(0, offsetValues.Count)];
    }

    private void ReduceRange()
    {
        maxDiff -= 1;
        minDiff -= 1;
    }
}
