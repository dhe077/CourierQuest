using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeytonExQuest : MonoBehaviour
{
    public ExerciseQuestManager exerciseQuestManager;

    public GameObject coldCoals;
    public GameObject warmCoals;
    public GameObject hotCoals;
    public GameObject bellowsTop;
    [SerializeField] private bool heating = false;
    [SerializeField] private float exerciseTime = 60;
    [SerializeField] private float speedMultiplier = 10;
    public float angleMin = 45f;
    public float angleMax = 315f;

    private float timer = 0;

    public void Update()
    {
        if (heating == true)
        {
            HeatCoals();
        }
    }

    public void StartHeating()
    {
        heating = true;
    }

    public void HeatCoals()
    {
        timer -= Time.deltaTime;
    
        //float rotationSpeed = exerciseQuestManager.GetPlayerView().GetComponent<PlayerMovement>().GetBikeSpeed() * speedMultiplier;
        float rotationSpeed = 30;
        float rotationAngle = Mathf.PingPong(Time.time * rotationSpeed, angleMax - angleMin) + angleMin;

        // Apply the rotation around the Y-axis
        bellowsTop.transform.rotation = Quaternion.Euler(rotationAngle, 0f, 0f);

        if (timer <= 0)
        {
            Debug.Log("Finished!");
            hotCoals.SetActive(true);
            warmCoals.SetActive(false);

            heating = false;
            ResetPlayer();
        } 
        else if (timer <= timer / 2 && timer > 0)
        {
            warmCoals.SetActive(true);
            coldCoals.SetActive(false);
        }
    }

    public void PreparePlayer()
    {
        timer = exerciseTime;

        Debug.Log("Player Prepared.");
    }

    private void ResetPlayer()
    {
        //exerciseQuestManager.GetPlayerView().GetComponent<SplineFollower>().followMode = SplineFollower.FollowMode.Uniform;
    }
}
