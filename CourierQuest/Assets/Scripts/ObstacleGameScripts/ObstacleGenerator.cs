using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] public GameObject playerView;
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] public Queue<GameObject> currentObstacles;

    private int zPosition = 0;
    public int maxSpread = 5;

    public bool startGenerating = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player view game object
        playerView = GameObject.Find("PlayerView");

        // Initialise queue
        currentObstacles = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startGenerating == true)
        {
            if (playerView.transform.position.z >= zPosition - 180)
            {
                GenerateObstacle();
                GenerateObstacle();
                GenerateObstacle();
            }
            if (currentObstacles.Count > 8)
            {
                DestroyObstacle();
                DestroyObstacle();
                DestroyObstacle();
            }
        }
    }

    private int ChooseForestPath()
    {
        System.Random rnd = new System.Random();
        int obstacleIndex = rnd.Next(0, obstaclePrefabs.Count);
        return obstacleIndex;
    }

    private Vector3 RandomPosition(int min, int max)
    {
        System.Random rnd = new System.Random();
        int zVal = rnd.Next(min, max);
        int xVal = rnd.Next(-maxSpread, maxSpread);
        Vector3 newPosition = new Vector3(xVal, 0.4f, zVal);
        return newPosition;
    }

    private void GenerateObstacle()
    {
        // Choose obstacle prefab
        int obstacleIndex = ChooseForestPath();
        // Get random obstacle from list
        GameObject nextObstacle = obstaclePrefabs[obstacleIndex];
        
        // Make a random Vector3 for the next position then move xPosition forward by 60
        //Vector3 nextPosition = new Vector3(0, 0, zPosition);
        Vector3 nextPosition = RandomPosition(zPosition, zPosition + 20);
        zPosition += 60;
        Quaternion noRotation = Quaternion.identity;
        
        // Instantiate obstacle at next position
        GameObject newObstacle = Instantiate(nextObstacle, nextPosition, noRotation);
        currentObstacles.Enqueue(newObstacle);
    }

    private void DestroyObstacle()
    {
        GameObject oldPath = currentObstacles.Dequeue();
        Destroy(oldPath);
    }
}
