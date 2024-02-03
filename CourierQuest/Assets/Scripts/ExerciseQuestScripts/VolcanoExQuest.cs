using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class VolcanoExQuest : MonoBehaviour
{
    public ExerciseQuestManager exerciseQuestManager;

    public GameObject followerObject;
    public SplineComputer playerPath;
    public SplineComputer followerObjectPath;
    [SerializeField] private bool moving = false;
    [SerializeField] private float followTime = 60;

    public void Update()
    {
        if (moving == true)
        {
            MoveUpVolcano();
        }
    }

    public void Start()
    {
        moving = true;
    }

    public void MoveUpVolcano()
    {
        SplineFollower playerSplineFollower = exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>();

        if (followerObject == null)
        {   
            if (playerSplineFollower.result.percent >= 1.0)
            {
                Debug.Log("At the top!");
                moving = false;
                ResetPlayer();
            }
        }
        else
        {
            followerObject.GetComponent<SplineFollower>().SetPercent(playerSplineFollower.result.percent);

            if (followerObject.GetComponent<SplineFollower>().result.percent >= 1.0)
            {
                Debug.Log("At the top!");
                moving = false;
                ResetPlayer();
            }
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
