using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private ObstacleGame obstacleGame;
    
    [Header("----Damage Flash----")]
    [SerializeField] private Graphic ouchImage;
    public Color flashColor = new Color(1.0f, 0.0f, 0.0f, 0.25f); // Red with 25% transparency
    public float flashDuration = 0.2f; // Adjust this value to control the flash duration
    public float fadeDuration = 0.5f; // Adjust this value to control the fade duration
    private bool isFlashing = false;

    // Check everytime a scene is loaded for the obstacle game
    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try { obstacleGame = GameObject.Find("ObstacleGameSystem").GetComponent<ObstacleGame>(); }
        catch(NullReferenceException) { Debug.Log("No ObstacleGame set in CollisionDetector!"); }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Hit {other.name}!");
        if (other.tag == "Obstacle")
        {
            obstacleGame.HitObstacle();
            StartFlash();
        }
        else if (other.name == "MainPath" || other.name == "OffPath")
        {
            obstacleGame.ForestSplitMain(other.name);
        }
    }

    public void SetObstacleGame(ObstacleGame oG)
    {
        obstacleGame = oG;
    }

    void StartFlash()
    {
        if (ouchImage != null)
        {
            // Apply the flash color
            ouchImage.color = flashColor;

            // Start the flash coroutine
            StartCoroutine(FlashAndFade());
        }
    }

    IEnumerator FlashAndFade()
    {
        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Calculate the rate of alpha change for fading
        float alphaChangeRate = 1.0f / fadeDuration;

        while (ouchImage.color.a > 0.0f)
        {
            Color currentColor = ouchImage.color;
            currentColor.a -= alphaChangeRate * Time.deltaTime;
            ouchImage.color = currentColor;

            yield return null;
        }

        // Ensure the final color is fully transparent
        Color transparentColor = flashColor;
        transparentColor.a = 0.0f;
        ouchImage.color = transparentColor;
    }
}
