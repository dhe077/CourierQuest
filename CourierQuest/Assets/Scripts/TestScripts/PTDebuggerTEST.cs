using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using System;
using Dreamteck.Splines;
using Unity.VisualScripting;

public class PTDebuggerTEST : MonoBehaviour
{
    public TextMeshProUGUI headRotationUI;
    public TextMeshProUGUI headPositionUI;

    private PositionTrackerTEST inputData;
    private Vector3 headRot = Vector3.zero;
    private Vector3 headPos = Vector3.zero;

    
    

    private float statusMaxSpeedMultiplier = 1f;
    [SerializeField] public float _headMoveMultiplier = 1;
    [SerializeField] public AnimationCurve _headXToMove;
    public GameObject playerView;
    public Rigidbody rb;
    public SplineFollower splineFollower;
    
    private float xOffset = 0.0f;
    public float maxDist = 5.0f;


    private void Start()
    {
        inputData = GetComponent<PositionTrackerTEST>();
        playerView = GameObject.Find("PlayerView");
        rb = playerView.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (inputData.head.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion rotation))
        // {
        //     headRot = rotation.eulerAngles;
        //     float x = Mathf.Round(headRot.x);
        //     float y = Mathf.Round(headRot.y);
        //     float z = Mathf.Round(headRot.z);
        //     headRotationUI.text = $"Rotation (x:{x}, y:{y}, z:{z})";
        // }
        // if (inputData.head.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 position))
        // {
        //     headPos = position;
        //     double x = Math.Round(headPos.x, 3);
        //     double y = Math.Round(headPos.y, 3);
        //     double z = Math.Round(headPos.z, 3);
        //     headPositionUI.text = $"Position (x:{x}, y:{y}, z:{z})";
        // }
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
            //     // Then multiply that position by the curve
            //     float headMoveMultiplier = _headMoveMultiplier * Mathf.Min(statusMaxSpeedMultiplier, 1);
            //     rb.velocity += new Vector3(_headXToMove.Evaluate(Mathf.Abs(headPos.x)) * headMoveMultiplier * Mathf.Sign(headPos.x), 0, 0);
            
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

            //headPositionUI.text = $"xOffset {xOffset}";
            
        }
    }
}
