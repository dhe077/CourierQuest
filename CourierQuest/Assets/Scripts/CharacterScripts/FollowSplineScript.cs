using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class FollowSplineScript : MonoBehaviour
{
    private SplineFollower splineFollower;

    private float speed = 0f;
    private float currentSplinePercent = 0f;
    [SerializeField] private float stopSpeed = 0f;
    [SerializeField] private float startSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        splineFollower = GetComponent<SplineFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSplinePercent = (float) splineFollower.result.percent;
        speed = Mathf.Lerp(startSpeed, stopSpeed, currentSplinePercent);
        splineFollower.followSpeed = speed;
    }
}
