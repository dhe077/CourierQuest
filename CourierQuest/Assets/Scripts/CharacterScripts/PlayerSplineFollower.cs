using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerSplineFollower : MonoBehaviour
{
    public SplineComputer path;
    private SplineFollower playerSplineFollower;
    private SplineFollower splineFollower;

    // Start is called before the first frame update
    void Start()
    {
        playerSplineFollower = GameObject.Find("PlayerView").GetComponent<SplineFollower>();
        splineFollower = GetComponent<SplineFollower>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerPercent = (float) playerSplineFollower.result.percent;
        splineFollower.SetPercent(playerPercent);
    }
}
