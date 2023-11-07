using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGame : MonoBehaviour
{
    [SerializeField] private GameObject playerView;
    [SerializeField] private StoryCommands storyCommands;

    public int obstaclesHit = 0;
    public int idealObstaclesHit = 10;

    private bool followedPath = true;

    private float spawnTimer = 0f;
    [SerializeField] private float timer = 0f;
    public float maxTime = 10.0f;
    private bool startTicking = false;

    private void Start()
    {
        playerView = GameObject.Find("PlayerView");
        storyCommands = GameObject.Find("Custom Dialogue System").GetComponent<StoryCommands>();

        startTicking = true;
    }

    private void Update()
    {
        // Timer
        if (startTicking == true) 
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {   
                startTicking = false;
                ObstacleGameOutcome();
            }
        }
    }

    private void ObstacleGameOutcome()
    {
        if (obstaclesHit < idealObstaclesHit && followedPath == true)
            storyCommands.StartFrom("Successfully_follow_the_path_and_avoids_obstacles");
        else if (obstaclesHit < idealObstaclesHit && followedPath == false)
            storyCommands.StartFrom("Unsuccessfully_follow_the_main_path");
        else if (obstaclesHit >= idealObstaclesHit && followedPath == true)
            storyCommands.StartFrom("Knocked_into_too_many_obstacles");
        else if (obstaclesHit >= idealObstaclesHit && followedPath == false)
            storyCommands.StartFrom("Got_lost_and_hit_too_many_obstacles");
    }
}
