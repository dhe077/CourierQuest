using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGame : MonoBehaviour
{
    [Header("----From PlayerView----")]
    [SerializeField] private StoryCommands storyCommands;

    [Header("----Forest Path Generation----")]
    [SerializeField] private ForestPathGeneration forestPathGeneration;
    [SerializeField] private int maxSpread = 5;
    [SerializeField] private int maxForwardSpread = 20;
    private bool followedPath = true;

    [Header("----Obstacle Generation----")]
    [SerializeField] private ObstacleGenerator obstacleGenerator;
    [SerializeField] private int obstaclesHit = 0;
    public int maxObstaclesHit = 10;

    [Header("----Timer Variables----")]
    [SerializeField] private float timer = 0f;
    [SerializeField] private float spawnTimer = 5.0f;
    public float maxTime = 20.0f;
    private bool startTicking = false;

    private void Start()
    {
        storyCommands = GameObject.Find("Custom Dialogue System").GetComponent<StoryCommands>();

        // Start generating forest paths
        forestPathGeneration.SetGenerate(true);

        // Setup obstacle variables
        SetUpObstacleVariables();

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
                // Stop timer and reset it
                startTicking = false;
                timer = 0f;

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
        if (obstaclesHit < maxObstaclesHit && followedPath == true)
            storyCommands.StartFrom("Successfully_follow_the_path_and_avoids_obstacles");
        else if (obstaclesHit < maxObstaclesHit && followedPath == false)
            storyCommands.StartFrom("Unsuccessfully_follow_the_main_path");
        else if (obstaclesHit >= maxObstaclesHit && followedPath == true)
            storyCommands.StartFrom("Knocked_into_too_many_obstacles");
        else if (obstaclesHit >= maxObstaclesHit && followedPath == false)
            storyCommands.StartFrom("Got_lost_and_hit_too_many_obstacles");
    }

    public void HitObstacle()
    {
        obstaclesHit += 1;
    }

    public void ForestSplitMain(string pathName)
    {
        forestPathGeneration.GenerateSpecificPath(pathName);
        if (pathName == "MainPath")
            followedPath = true;
        else if (pathName == "OffPath")
            followedPath = false;
        ObstacleGameOutcome();
    }

    public void SetUpObstacleVariables()
    {
        obstacleGenerator.SetMaxSpread(maxSpread);
        obstacleGenerator.SetMaxForwardSpread(maxForwardSpread);
    }


}
