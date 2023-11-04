using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerViewManager : MonoBehaviour
{
    [SerializeField] private GameObject playerView;
    [SerializeField] private List<SplineComputer> splineComputers;
    public int positionIndex = 0;
    [SerializeField] public float moveSpeedMultiplier = 10.0f;

    void Start()
    {
        playerView = GameObject.Find("PlayerView");

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
        SetSplinePath();
    }
}
