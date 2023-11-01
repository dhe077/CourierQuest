using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerViewManager : MonoBehaviour
{
    [SerializeField] private GameObject playerView;
    [SerializeField] private Transform startingPosition;
    [SerializeField] private SplineComputer splineComputer;

    void Start()
    {
        playerView = GameObject.Find("PlayerView");

        SetStartingPosition();
        SetSplinePath();
    }

    private void SetStartingPosition()
    {
        playerView.transform.position = startingPosition.position;
        playerView.transform.rotation = startingPosition.rotation;
    }

    private void SetSplinePath()
    {
        playerView.GetComponent<SplineFollower>().spline = splineComputer;
    }
}
