using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class KeepTextInView : MonoBehaviour
{
    private PositionTracker inputData;
    private Vector3 headPos = Vector3.zero;
    [SerializeField] private Vector3 transHeadPos = Vector3.zero;
    public GameObject dialogueObject;

    [SerializeField] private Camera headCamera;

    public float offset = 30f;

    // Start is called before the first frame update
    private void Start()
    {
        inputData = GetComponent<PositionTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (inputData.head.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 position))
        // {
        //     // Position of the head
        //     //headPos = position;
        //     transHeadPos = transform.TransformPoint(position);

        //     dialogueObject.transform.position = new Vector3(0, transHeadPos.y, 0);
        // }

        Vector3 newPos = headCamera.transform.position;
        Vector3 oldPos = dialogueObject.transform.position;
        dialogueObject.transform.position = new Vector3(oldPos.x, newPos.y + 0.2f, oldPos.z);
    }
}
