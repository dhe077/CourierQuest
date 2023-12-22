using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class ThakaliExQuest : MonoBehaviour
{
    public ExerciseQuestManager exerciseQuestManager;

    public GameObject plow;
    public SplineComputer playerPath;
    public SplineComputer plowPath;
    [SerializeField] private bool plowing = false;
    [SerializeField] private float followTime = 60;

    public void Update()
    {
        if (plowing == true)
        {
            PlowField();
        }
    }

    public void StartPlowing()
    {
        plowing = true;
    }

    public void PlowField()
    {
        SplineFollower playerSplineFollower = exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>();
        plow.GetComponent<SplineFollower>().SetPercent(playerSplineFollower.result.percent);

        if (plow.GetComponent<SplineFollower>().result.percent >= 1.0)
        {
            Debug.Log("Field Plowed!");
            plowing = false;
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
