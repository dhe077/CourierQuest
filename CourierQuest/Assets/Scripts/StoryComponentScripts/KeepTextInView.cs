using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class KeepTextInView : MonoBehaviour
{
    public GameObject playerObject;
    public float moveSpeed = 1f;

    [SerializeField] private Transform headCamera;

    public Vector3 offsetVector = new Vector3(0f, 0f, 0f);


    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = offsetVector + headCamera.transform.localPosition;

        // Debug.Log(targetPosition);

        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    }
}
