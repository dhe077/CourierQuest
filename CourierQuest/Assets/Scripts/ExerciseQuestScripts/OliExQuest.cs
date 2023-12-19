using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class OliExQuest : MonoBehaviour
{
    public ExerciseQuestManager exerciseQuestManager;

    public GameObject slime;
    public SplineComputer playerPath;
    public SplineComputer slimeViewPath;
    [SerializeField] private bool escaping = false;
    [SerializeField] private float followTime = 60;

    public void Update()
    {
        if (escaping == true)
        {
            EscapeSlime();
        }
    }

    public void StartEscaping()
    {
        escaping = true;
    }

    public void EscapeSlime()
    {
        SplineFollower playerSplineFollower = exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>();
        slime.GetComponent<SplineFollower>().SetPercent(playerSplineFollower.result.percent);

        if (slime.GetComponent<SplineFollower>().result.percent >= 1.0)
        {
            Debug.Log("Escaped!");
            escaping = false;
            ResetPlayer();
        }
    }

    public void PreparePlayer()
    {
        // Set the follow mode to time so we can control how long the player pedals for
        exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().followMode = SplineFollower.FollowMode.Time;

        // Set the duration of the exercise
        exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().followDuration = followTime;
        Debug.Log("Exercise details ready.");
    }

    private void ResetPlayer()
    {
        exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().followMode = SplineFollower.FollowMode.Uniform;
    }
}
