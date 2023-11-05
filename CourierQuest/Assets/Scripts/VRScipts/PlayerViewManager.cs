using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.UI;

public class PlayerViewManager : MonoBehaviour
{
    [SerializeField] private GameObject playerView;
    [SerializeField] private List<SplineComputer> splineComputers;
    public int positionIndex = 0;
    [SerializeField] public float moveSpeedMultiplier = 10.0f;

    // For spot fading
    public Image image;
    public float fadeDuration = 1.0f;

    private bool isFading = false;

    void Start()
    {
        playerView = GameObject.Find("PlayerView");

        // Get the spot fade image
        image = GameObject.Find("SpotChangeImage").GetComponent<Image>();
        // Set the initial alpha of the Image to fully transparent
        SetImageAlpha(image, 0f);

        SetSplinePath();
        SetMoveSpeedMultiplier();
    }

    private void SetSplinePath()
    {
        playerView.GetComponent<SplineFollower>().spline = splineComputers[positionIndex];
    }

    private void SetMoveSpeedMultiplier()
    {
        playerView.GetComponent<PlayerMovement>().SetMoveSpeed(moveSpeedMultiplier);
    }

    public void NextPosition()
    {
        positionIndex += 1;
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
            StartCoroutine(FadeInAndOut(image, fadeDuration));
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
}
