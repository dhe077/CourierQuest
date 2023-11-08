using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class KeepTextInView : MonoBehaviour
{
    private PositionTracker inputData;
    private Vector3 headPos = Vector3.zero;
    public GameObject dialogueObject;

    public float offset = 30f;

    // Start is called before the first frame update
    private void Start()
    {
        inputData = GetComponent<PositionTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputData.head.TryGetFeatureValue(CommonUsages.centerEyePosition, out Vector3 position))
        {
            // Position of the head
            headPos = position;

            dialogueObject.transform.position = new Vector3(0, headPos.y + offset, 0.6f);
        }
    }
}
