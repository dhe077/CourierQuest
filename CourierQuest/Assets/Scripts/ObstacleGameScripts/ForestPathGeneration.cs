using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ForestPathGeneration : MonoBehaviour
{
    [SerializeField] public GameObject playerView;
    [SerializeField] public List<GameObject> forestPathPrefabs;
    [SerializeField] public GameObject forestPathSplit;
    [SerializeField] public GameObject forestMainPath;
    [SerializeField] public GameObject forestOffPath;

    [SerializeField] public Queue<GameObject> currentForestPaths;
    
    
    // This is the x position of the prefabs when intantiating them
    // Should increase by 60 every time
    private int zPosition = 0;
    private bool startGenerating = true;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player view game object
        playerView = GameObject.Find("PlayerView");

        // Initialise queue
        currentForestPaths = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerView.transform.position);
        if (startGenerating == true)
        {
            if (playerView.transform.position.z >= zPosition - 180)
            {
                GeneratePath();
                GeneratePath();
                GeneratePath();
            }
            if (currentForestPaths.Count > 8)
            {
                DestroyPath();
                DestroyPath();
                DestroyPath();
            }
        }
    }

    public void SetGenerate(bool generate)
    {
        startGenerating = generate;
    }

    private int ChooseForestPath()
    {
        System.Random rnd = new System.Random();
        int pathIndex = rnd.Next(0, forestPathPrefabs.Count);
        return pathIndex;
    }

    private void GeneratePath()
    {
        // Choose path prefab
        int pathIndex = ChooseForestPath();
        // Get next path from list
        GameObject nextPath = forestPathPrefabs[pathIndex];
        // Make a Vector3 for the next position and move xPosition forward 60 and 
        Vector3 nextPosition = new Vector3(0, 0, zPosition);
        zPosition += 60;
        Quaternion noRotation = Quaternion.identity;
        // Instantiate path at next position
        GameObject newPath = Instantiate(nextPath, nextPosition, noRotation);
        currentForestPaths.Enqueue(newPath);
    }

    private void DestroyPath()
    {
        GameObject oldPath = currentForestPaths.Dequeue();
        Destroy(oldPath);
    }

    //public void DestroyAllPaths(){}

    public void GenerateSplitPath()
    {
        // Use the split path prefab
        GameObject nextPath = forestPathSplit;
        // Make a Vector3 for the next position and move xPosition forward 
        zPosition += 30;
        Vector3 nextPosition = new Vector3(0, 0, zPosition);
        zPosition += 90;
        Quaternion noRotation = Quaternion.identity;
        // Instantiate path at next position
        GameObject newPath = Instantiate(nextPath, nextPosition, noRotation);
        currentForestPaths.Enqueue(newPath);
    }

    public void GenerateSpecificPath(string path)
    {
        // Generate a MainPath specific path
        // Use the split path prefab
        GameObject nextPath = null;
        if (path == "MainPath")
            nextPath = forestMainPath;
        else if (path == "OffPath")
            nextPath = forestOffPath;

        // Make a Vector3 for the next position and move xPosition forward 60 and 
        Vector3 nextPosition = new Vector3(0, 0, zPosition);
        zPosition += 60;
        Quaternion noRotation = Quaternion.identity;
        // Instantiate path at next position
        GameObject newPath = Instantiate(nextPath, nextPosition, noRotation);
        currentForestPaths.Enqueue(newPath);

        // Generate the normal paths
        SetGenerate(true);
    }
}
