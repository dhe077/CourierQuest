using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class KeepTextInView : MonoBehaviour
{
    public GameObject dialogueObject;

    [SerializeField] private Camera headCamera;

    public float offset = 0.2f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = headCamera.transform.position;
        Vector3 oldPos = dialogueObject.transform.position;
        dialogueObject.transform.position = new Vector3(oldPos.x, newPos.y + offset, oldPos.z);
    }
}
