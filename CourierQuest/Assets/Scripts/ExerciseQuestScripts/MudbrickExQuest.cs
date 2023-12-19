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

    //private float oldMoveSpeed;
    //[SerializeField] private float newMoveSpeed = 1;
    [SerializeField] private float followTime = 60;

    public void Update()
    {
        if (exerciseQuestManager.GetStartQuest() == true)
        {
            //PlayerMovement playerMovement = exerciseQuestManager.GetPlayerView().GetComponent<PlayerMovement>();
            //oldMoveSpeed = playerMovement.GetMoveSpeedMultiplier();
            //playerMovement.SetMoveSpeed(newMoveSpeed);

            // Set the follow mode to time so we can control how long the player pedals for
            exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().followMode = SplineFollower.FollowMode.Time;

            // Set the duration of the exercise
            exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().followDuration = followTime;

            // Set the offset rotation so that the camera faces the statue
            exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().motion.rotationOffset = new Vector3(0, 180, 0);

            Debug.Log("Exercise details ready.");

            lifting = true;
            exerciseQuestManager.SetStartQuest(false);
        }
        
        if (lifting == true)
        {
            LiftStatue();
        }
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

    private void ResetPlayer()
    {
        //PlayerMovement playerMovement = exerciseQuestManager.GetPlayerView().GetComponent<PlayerMovement>();
        //playerMovement.SetMoveSpeed(oldMoveSpeed);

        exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().followMode = SplineFollower.FollowMode.Uniform;
        exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().motion.rotationOffset = new Vector3(0, 0, 0);
    }
}
