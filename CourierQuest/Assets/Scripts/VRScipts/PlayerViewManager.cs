using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewManager : MonoBehaviour
{
    [Header("----Player View Object----")]
    [SerializeField] private GameObject playerView;
    //[SerializeField] private bool detectCollisions = false;

    [Header("----Spline Computers----")]
    [SerializeField] private List<SplineComputer> splineComputers;
    private int positionIndex = 0;

    [Header("----Player Movement----")]
    [SerializeField] public float moveSpeedMultiplier = 10.0f;
    
    [Header("----Head Position Manager----")]
    [SerializeField] public bool enableHeadMovement = false;
    [SerializeField] private float maxDist = 5.0f;
    [SerializeField] public GameObject calibrationTarget;

    [Header("----Spot Change----")]
    [SerializeField] private Image spotFadeImage; // For spot fading
    [SerializeField] public float fadeDuration = 1.0f;

    private bool isFading = false;

    void Start()
    {
        playerView = GameObject.Find("PlayerView");

        //SetCalibrationTarget();

        SetSplinePath();

        // Set PositionTracker variables
        SetPositionTracker();

        // Set PlayerMovement variables
        SetMoveSpeedMultiplier();

        // Set the initial alpha of the Image to fully transparent
        spotFadeImage = GameObject.Find("SpotChangeImage").GetComponent<Image>();
        SetImageAlpha(spotFadeImage, 0f);
    }

    private void SetSplinePath()
    {
        playerView.GetComponent<SplineFollower>().spline = splineComputers[positionIndex];
        playerView.GetComponent<SplineFollower>().SetDistance(0);
        playerView.GetComponent<SplineFollower>().motion.offset = new Vector2(0, 0);
    }

    private void SetMoveSpeedMultiplier()
    {
        playerView.GetComponent<PlayerMovement>().SetMoveSpeed(moveSpeedMultiplier);
    }

    private void SetPositionTracker()
    {
        // Enable the playerView PositionTracker if enableHeadMovement is true
        GameObject positionTracker = GameObject.Find("PositionTracker");
        positionTracker.SetActive(enableHeadMovement);
        if (positionTracker.activeSelf == true)
        {
            // Set PositionTracker variables
            HeadPositionManager headPositionManager = positionTracker.GetComponent<HeadPositionManager>();
            headPositionManager.SetMaxDist(maxDist);
            headPositionManager.playerView = playerView;
            headPositionManager.splineFollower = playerView.GetComponent<SplineFollower>();
        }
    }

    // private void SetCalibrationTarget()
    // {
    //     playerView.GetComponent<Calibration>().target = calibrationTarget.transform;
    //     playerView.GetComponent<Calibration>().Recenter();
    // }

    public void NextPosition()
    {
        positionIndex += 1;
        StartFading();
    }

    public void SpecificPosition(string splineName)
    {
        SplineComputer spline = GameObject.Find(splineName).GetComponent<SplineComputer>();
        int index = splineComputers.IndexOf(spline);
        positionIndex = index;
        StartFading();
    }

    ////
    // This section of code for fading in and out is a real mess
    ////

    public void StartFading()
    {
        if (!isFading)
        {
            isFading = true;
            StartCoroutine(FadeInAndOut(spotFadeImage, fadeDuration));
        }
    }

    private IEnumerator FadeInAndOut(Image img, float duration)
    {
        // Fade in
        yield return FadeImage(img, 0, 1, duration);

        // Change Spots
        SetSplinePath();

        // Wait for a delay
        yield return new WaitForSeconds(duration);

        // Fade out
        yield return FadeImage(img, 1, 0, duration);

        isFading = false; // Reset the flag so that it can be called again
    }

    private IEnumerator FadeImage(Image img, float startAlpha, float targetAlpha, float duration)
    {
        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            SetImageAlpha(img, Mathf.Lerp(startAlpha, targetAlpha, t));
            yield return null;
        }

        SetImageAlpha(img, targetAlpha);
    }

    private void SetImageAlpha(Image img, float alpha)
    {
        Color imageColor = img.color;
        imageColor.a = alpha;
        img.color = imageColor;
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
