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
    public GameObject chaseObject;
    
    [Header("----Environmental----")]
    public GameObject lightning;
    public float decreaseRate = 0.2f;  // Rate at which the lightning intensity lowers.
    public GameObject rainGroup;
    public float increaseRate = 0.2f;
    public List<GameObject> activatableObjects;
    
    
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
        try
        {
            playerChaser.StartChasing(playerView, chaseObject);
        }
        catch (NullReferenceException)
        {
            string debugString = "PlayerChaser not set - Reason: ";
            if (playerView == null)
                debugString += "playerView null";
            else if (chaseObject == null)
                debugString += "chaseObject null";
            Debug.Log(debugString);
        }
        
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

    public void EnableObject(string enableObj, bool enableBool)
    {
        GameObject enableGroup = FindObjectInList(enableObj);
        enableGroup.SetActive(enableBool);
    }

    public void EnableAndDisableObjects(string enableObj, string disableObj)
    {
        GameObject disableGroup = FindObjectInList(disableObj);
        disableGroup.SetActive(false);

        GameObject enableGroup = FindObjectInList(enableObj);
        enableGroup.SetActive(true);
        
    }

    public GameObject FindObjectInList(string objName)
    {
        for (int i = 0; i < activatableObjects.Count; i++)
        {
            if (activatableObjects[i].name == objName)
                return activatableObjects[i];
        }
        Debug.Log("Error: Object not found in list!");
        return null;
    }

    public void RideHorse(bool hopOn)
    {
        playerView = GameObject.Find("PlayerView");
        if (playerView != null)
        {
            GameObject horseGroup = GameObject.Find("PlayerStormhoof");
            horseGroup.GetComponent<HorseScript>().EnableHorse(hopOn);
        }
        else
        {
            Debug.Log("No playerView found!");
        }
    }
}
