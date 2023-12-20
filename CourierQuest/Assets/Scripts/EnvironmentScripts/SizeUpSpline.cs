using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SizeUpSpline : MonoBehaviour
{
    public SplineFollower splineFollower;
    public float sizeModifier = 10;

    public float initialSize = 0.1f;
    public float maxSize = 1.0f;

    public void Update()
    {
        float t = (float) splineFollower.result.percent; // normalized position along the spline
        float scaleFactor = Mathf.Lerp(initialSize, maxSize, t);

        // Apply the scale factor to the object
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }
}
