using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class MudbrickExQuest : MonoBehaviour
{
    public ExerciseQuestManager exerciseQuestManager;

    public GameObject statue;
    public SplineComputer statuePath;
    public SplineComputer playerPath;
    [SerializeField] private bool lifting = false;
    [SerializeField] private float followTime = 60;

    public void Update()
    {
        if (lifting == true)
        {
            LiftStatue();
        }
    }

    public void StartLifting()
    {
        lifting = true;
    }

    public void LiftStatue()
    {
        SplineFollower playerSplineFollower = exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>();
        statue.GetComponent<SplineFollower>().SetPercent(playerSplineFollower.result.percent);

        if (statue.GetComponent<SplineFollower>().result.percent >= 1.0)
        {
            Debug.Log("Stop Lifting!");
            lifting = false;
            ResetPlayer();
        }
    }

    public void PreparePlayer()
    {
        // Set the follow mode to time so we can control how long the player pedals for
        exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().followMode = SplineFollower.FollowMode.Time;

        // Set the duration of the exercise
        exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().followDuration = followTime;
        Debug.Log("Player Prepared.");
    }

    private void ResetPlayer()
    {
        exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().followMode = SplineFollower.FollowMode.Uniform;
    }
}
