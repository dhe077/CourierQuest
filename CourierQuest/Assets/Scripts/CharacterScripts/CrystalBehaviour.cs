using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBehaviour : MonoBehaviour
{
    public GameObject playerView;
    [SerializeField] private float timer;
    [SerializeField] private float maxTime = 30;
    private float thirdTime;
    private string blinkingColour = "green";
    [SerializeField] private float pingPongSpeed = 1.0f;

    [Header ("----Objects----")]
    [SerializeField] private GameObject greenColored;
    [SerializeField] private GameObject orangeColored;
    [SerializeField] private GameObject redColored;
    [SerializeField] private GameObject greenTransparent;
    [SerializeField] private GameObject orangeTransparent;
    [SerializeField] private GameObject redTransparent;

    [Header ("----Materials----")]
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material orangeMaterial;
    [SerializeField] private Material redMaterial;

    [Header ("----Colors----")]
    [SerializeField] private Color targetGreenColor;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color targetOrangeColor;
    [SerializeField] private Color orangeColor;
    [SerializeField] private Color targetRedColor;
    [SerializeField] private Color redColor;

    // Start is called before the first frame update
    void Start()
    {
        thirdTime = maxTime / 3;
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            ResetTimer();

        TimerCalculator();
        
        StartCoroutine(BlinkMaterial());

        if (playerView.GetComponent<PlayerMovement>().GetBikeSpeed() > 0)
            timer = Mathf.Clamp(timer + Time.deltaTime, 0, maxTime);
        else
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, maxTime);
    }

    private void TimerCalculator()
    {
        if (timer > 0 && timer < thirdTime) // red
        {
            greenColored.SetActive(false);
            greenTransparent.SetActive(true);

            orangeColored.SetActive(false);
            orangeTransparent.SetActive(true);

            redColored.SetActive(true);

            blinkingColour = "red";
        }
        else if (timer > thirdTime && timer < thirdTime * 2) // red, orange
        {
            greenColored.SetActive(false);
            greenTransparent.SetActive(true);

            orangeColored.SetActive(true);
            orangeTransparent.SetActive(false);

            redColored.SetActive(true);

            blinkingColour = "orange";
        }
        else if (timer > thirdTime * 2 && timer < thirdTime * 3) // red, orange, green
        {
            greenColored.SetActive(true);
            greenTransparent.SetActive(false);

            orangeColored.SetActive(true);
            orangeTransparent.SetActive(false);

            redColored.SetActive(true);
            redTransparent.SetActive(false);

            blinkingColour = "green";
        }
    }

    IEnumerator BlinkMaterial()
    {
        float intensity = Mathf.PingPong(Time.time * pingPongSpeed, 1);

        switch (blinkingColour)
        {
            case "green":
                SetEmissionColor(greenColor, targetGreenColor, greenMaterial, intensity);
                break;
            case "orange":
                SetEmissionColor(orangeColor, targetOrangeColor, orangeMaterial, intensity);
                break;
            case "red":
                SetEmissionColor(redColor, targetRedColor, redMaterial, intensity);
                break;
        }

        yield return null;
    }

    private void SetEmissionColor(Color originalColor, Color blinkColor, Material material, float intensity)
    {
        Color color = material.GetColor("_EmissionColor");

        if (!IsSameColor(color, blinkColor))
        {
            //Debug.Log($"Not Same");
            Color lerpedColor = Color.Lerp(originalColor, blinkColor, intensity);
            material.SetColor("_EmissionColor", lerpedColor);
        }
        else
        {   
            //Debug.Log($"Same");
            Color lerpedColor = Color.Lerp(blinkColor, originalColor, intensity);
            material.SetColor("_EmissionColor", lerpedColor);
        } 
    }

    private bool IsSameColor(Color x, Color y)
    {
        int xR = (int) x.r;
        int xG = (int) x.g;
        int xB = (int) x.b;

        int yR = (int) y.r;
        int yG = (int) y.g;
        int yB = (int) y.b;

        if (xR == yR)
            if (xG == yG)
                if (xB == yB)
                    return true;
        return false;

    }

    private void ResetTimer()
    {
        timer = maxTime;
    }
}
