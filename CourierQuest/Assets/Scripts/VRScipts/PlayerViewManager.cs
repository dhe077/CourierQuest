using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerViewManager : MonoBehaviour
{
    [SerializeField] private GameObject playerView;
    [SerializeField] private Transform startingPosition;
    [SerializeField] private SplineComputer splineComputer;
    [SerializeField] public float moveSpeedMultiplier = 10.0f;

    void Start()
    {
        playerView = GameObject.Find("PlayerView");

        SetStartingPosition();
        SetSplinePath();
        SetMoveSpeedMultiplier();
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

    private void SetMoveSpeedMultiplier()
    {
        playerView.GetComponent<PlayerMovement>().SetMoveSpeed(moveSpeedMultiplier);
    }
}
