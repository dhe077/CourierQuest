using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class FinnExQuest : MonoBehaviour
{
    public ExerciseQuestManager exerciseQuestManager;

    public GameObject magicWheel;
    public GameObject bulbs;
    public GameObject greenBulbs;
    [SerializeField] private bool generating = false;
    [SerializeField] private float exerciseTime = 60;
    [SerializeField] private float speedMultiplier = 10;

    private float timer = 0;

    public void Update()
    {
        if (generating == true)
        {
            GenerateMagic();
        }
    }

    public void StartGenerating()
    {
        generating = true;
    }

    public void GenerateMagic()
    {
        timer -= Time.deltaTime;

        float rotationSpeed = exerciseQuestManager.GetPlayerView().GetComponent<PlayerMovement>().GetBikeSpeed() * speedMultiplier;
        // magicWheel.transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        magicWheel.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (timer <= 0)
        {
            Debug.Log("Finished!");
            greenBulbs.SetActive(true);
            bulbs.SetActive(false);

            generating = false;
            ResetPlayer();
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
