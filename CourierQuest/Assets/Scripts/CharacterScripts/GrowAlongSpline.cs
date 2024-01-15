using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class GrowAlongSpline : MonoBehaviour
{
    private SplineFollower splineFollower;
    public float startSize = 1.0f;
    public float maxSize = 3.0f;

    void Start()
    {
        splineFollower = GetComponent<SplineFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the normalized distance along the spline
        float percent = (float) splineFollower.result.percent;

        // Calculate the size based on the percentage along the spline
        float newSize = Mathf.Lerp(startSize, maxSize, percent);

        // Set the object's scale to the calculated size
        transform.localScale = new Vector3(newSize, newSize, newSize);
    }
}
