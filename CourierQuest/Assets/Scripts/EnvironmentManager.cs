using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private SpawnPackages spawnPackages;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnPackages = GetComponent<SpawnPackages>();
    }

    public void SpawnPackageObjects()
    {
        spawnPackages.SpawnPackageObjects();
    }
}
