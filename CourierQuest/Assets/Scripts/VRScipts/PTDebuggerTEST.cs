using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using System;

public class PTDebuggerTEST : MonoBehaviour
{
    public TextMeshProUGUI headRotationUI;
    public TextMeshProUGUI headPositionUI;

    private PositionTrackerTEST inputData;
    private Vector3 headRot = Vector3.zero;
    private Vector3 headPos = Vector3.zero;

    private void Start()
    {
        inputData = GetComponent<PositionTrackerTEST>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputData.head.TryGetFeatureValue(CommonUsages.centerEyeRotation, out Quaternion rotation))
        {
            headRot = rotation.eulerAngles;
            float x = Mathf.Round(headRot.x);
            float y = Mathf.Round(headRot.y);
            float z = Mathf.Round(headRot.z);
            headRotationUI.text = String.Format("Rotation (x:{0}, y:{1}, z:{2})", x, y, z);
        }
        if (inputData.head.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 position))
        {
            headPos = position;
            float x = Mathf.Round(headPos.x);
            float y = Mathf.Round(headPos.y);
            float z = Mathf.Round(headPos.z);
            headPositionUI.text = String.Format("Position (x:{0}, y:{1}, z:{2})", x, y, z);
        }
    }
}
