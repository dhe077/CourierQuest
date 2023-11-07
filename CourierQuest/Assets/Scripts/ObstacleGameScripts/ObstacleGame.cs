using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGame : MonoBehaviour
{
    [SerializeField] private GameObject playerView;
    [SerializeField] private StoryCommands storyCommands;
    [SerializeField] private ObstacleGenerator obstacleGenerator;
    [SerializeField] private ForestPathGeneration forestPathGeneration;

    [SerializeField] private int obstaclesHit = 0;
    public int idealObstaclesHit = 10;

    private bool followedPath = true;

    private float spawnTimer = 5.0f;
    [SerializeField] private float timer = 0f;
    public float maxTime = 20.0f;
    private bool startTicking = false;

    private void Start()
    {
        playerView = GameObject.Find("PlayerView");
        storyCommands = GameObject.Find("Custom Dialogue System").GetComponent<StoryCommands>();

        // Start generating forest paths
        forestPathGeneration.SetGenerate(true);

        // Start the scene timer
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
                // Stop generating forest and clear obstacles
                forestPathGeneration.SetGenerate(false);
                obstacleGenerator.SetGenerate(false);
                obstacleGenerator.DestroyAllObstacles();

                // Generate the split path
                forestPathGeneration.GenerateSplitPath();
            }
            else if (timer >= spawnTimer)
            {
                obstacleGenerator.SetGenerate(true);
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

    public void HitObstacle()
    {
        obstaclesHit += 1;
    }


}
