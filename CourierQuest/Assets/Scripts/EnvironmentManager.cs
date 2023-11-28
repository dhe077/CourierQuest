using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.RainMaker;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private GameObject playerView;

    [Header("----Courier Guildhall----")]
    public bool enableSpawnPackages;
    private SpawnPackages spawnPackages;

    [Header("----Outrun Goblin----")]
    public bool enablePlayerChaser;
    private PlayerChaser playerChaser;
    public GameObject goblin;
    
    [Header("----Environmental----")]
    public GameObject lightning;
    public float decreaseRate = 0.2f;  // Rate at which the lightning intensity lowers.
    public GameObject rainGroup;
    public float increaseRate = 0.2f;
    
    
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

    void Update()
    {
        DecreaseLightningIntensity();
        IncreaseRainIntensity();
    }

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

    public void MakeLightning()
    {
        lightning.SetActive(true);
    }

    public void DecreaseLightningIntensity()
    {
        try
        {
            if (lightning.activeSelf == true)
            {
                Light lights = lightning.GetComponent<Light>();
                // Lower the intensity gradually
                lights.intensity = Mathf.Max(0f, lights.intensity - decreaseRate * Time.deltaTime);
                if (lights.intensity <= 0)
                {
                    lightning.SetActive(false);
                    lights.intensity = 2;
                }
            }
        } catch (NullReferenceException) { Debug.Log("No Light set in EnvironmentManager!"); }
        catch (UnassignedReferenceException) { Debug.Log("No Light set in EnvironmentManager!"); }
    }

    public void MakeRain(bool rainSwitch)
    {
        rainGroup.SetActive(rainSwitch);
    }

    public void IncreaseRainIntensity()
    {
        try
        {
            if (rainGroup.activeSelf == true)
            {
                RainScript rainScript = rainGroup.transform.GetChild(0).GetComponent<RainScript>();
                // Lower the intensity gradually
                rainScript.RainIntensity = Mathf.Min(1, rainScript.RainIntensity + increaseRate * Time.deltaTime);
            }
        } catch (NullReferenceException) { Debug.Log("No Rain set in EnvironmentManager!"); }
        catch (UnassignedReferenceException) { Debug.Log("No Rain set in EnvironmentManager!"); }
    }
}
