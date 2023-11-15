using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private GameObject playerView;

    [Header("Courier Guildhall")]
    public bool enableSpawnPackages;
    private SpawnPackages spawnPackages;

    [Header("Outrun Goblin")]
    public bool enablePlayerChaser;
    private PlayerChaser playerChaser;
    public GameObject goblin;
    //public bool testChase = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (enableSpawnPackages == true)
            spawnPackages = GetComponent<SpawnPackages>();
        
        if (enablePlayerChaser == true)
        {
            playerView = GameObject.Find("PlayerView");
            playerChaser = GetComponent<PlayerChaser>();
        }
    }

    // void Update()
    // {
    //     if (testChase == true)
    //     {
    //         StartChasing();
    //         testChase = false;
    //     } 
    // }

    public void SpawnPackageObjects()
    {
        spawnPackages.SpawnPackageObjects();
    }

    public void StartChasing()
    {
        playerChaser.StartChasing(playerView, goblin);
    }

    public void SetAtPlayerSide(bool x)
    {
        playerChaser.SetAtPlayerSide(x);
    }
}
