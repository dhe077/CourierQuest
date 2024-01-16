using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    public Vector3 minScale = new Vector3(1f, 1f, 1f);
    public Vector3 maxScale = new Vector3(2f, 2f, 2f);
    public float changeInterval = 1.0f; // Time interval between scale changes

    private Vector3 targetScale;
    private float lerpTime = 0.0f;

    void Start()
    {
        // Initialize the first target scale
        SetRandomTargetScale();
    }

    void Update()
    {
        // Increment the lerpTime
        lerpTime += Time.deltaTime;

        // Lerp between the current scale and the target scale
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lerpTime);

        // Check if the lerp is complete
        if (lerpTime >= changeInterval)
        {
            // Reset the lerpTime
            lerpTime = 0.0f;

            // Set a new random target scale
            SetRandomTargetScale();
        }
    }

    void SetRandomTargetScale()
    {
        // Generate random scale values
        float randomX = Random.Range(minScale.x, maxScale.x);
        float randomY = Random.Range(minScale.y, maxScale.y);
        float randomZ = Random.Range(minScale.z, maxScale.z);

        // Set the new random target scale
        targetScale = new Vector3(randomX, randomY, randomZ);
    }
}
