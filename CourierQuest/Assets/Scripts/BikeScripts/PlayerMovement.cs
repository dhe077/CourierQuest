using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{
    public BikeController bikeController;
    public float bikeSpeed = 0.0f;
    public int bikeRPM = 0;
    public int bikeHeartRate = 0;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI rpmText;
    public TextMeshProUGUI heartRateText;

    public float moveSpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        ReadBikeData();
        Debug.Log($"Speed: {bikeSpeed}");
        speedText.text = $"Speed: {bikeSpeed}";

        Debug.Log($"RPM: {bikeRPM}");
        rpmText.text = $"RPM: {bikeRPM}";

        Debug.Log($"Heart rate: {bikeHeartRate}");
        heartRateText.text = $"Heart rate: {bikeHeartRate}";
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    // This function pulls the data from the bike controller class
    public void ReadBikeData()
    {
        bikeSpeed = bikeController.speed;
        bikeRPM = bikeController.RPM;
        bikeHeartRate = bikeController.heartRate;
    }

    // To test the speed of movement.
    public void MovePlayer()
    {
        Vector3 movement = new Vector3(0.0f, 0.0f, bikeSpeed) * moveSpeed * Time.deltaTime;

        // Move the player
        transform.Translate(movement);
    }
}
