using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;
using Dreamteck.Splines;

public class HeadPositionManager : MonoBehaviour
{
    private PositionTracker inputData;
    private Vector3 headPos = Vector3.zero;

    public GameObject playerView;
    public SplineFollower splineFollower;

    private float statusMaxSpeedMultiplier = 1f;
    [SerializeField] private float _headMoveMultiplier = 1;
    [SerializeField] public AnimationCurve _headXToMove;
    
    private float xOffset = 0.0f;
    private float maxDist = 5.0f;


    private void Start()
    {
        inputData = GetComponent<PositionTracker>();
    }

    void FixedUpdate()
    {
        SetSideVelocity();
    }

    private void SetSideVelocity()
    {
        if (inputData.head.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 position))
        {
            // Position of the head
            headPos = position;

            // Then multiply that position by the curve
            float headMoveMultiplier = _headMoveMultiplier * Mathf.Min(statusMaxSpeedMultiplier, 1);
            Vector3 offsetChange = new Vector3(_headXToMove.Evaluate(Mathf.Abs(headPos.x)) * headMoveMultiplier * Mathf.Sign(headPos.x), 0, 0);
            xOffset += offsetChange.x;
            if (Math.Abs(xOffset) >= maxDist)
            {
                if (Mathf.Sign(xOffset) == 1)
                    xOffset = maxDist;
                else if (Mathf.Sign(xOffset) == -1)
                    xOffset = -maxDist; 
            }

            // Offset the x from the spline
            splineFollower.motion.offset = new Vector2(xOffset, 0);
        }
    }

    public void SetMaxDist(float newMaxDist)
    {
        maxDist = newMaxDist;
    }
}
