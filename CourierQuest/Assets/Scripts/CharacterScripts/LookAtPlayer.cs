using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject playerHead;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            playerHead = GameObject.Find("PlayerView").GetComponent<PlayerViewObjects>().GetMainCamera();
        }
        catch (NullReferenceException)
        {
            Debug.Log("PlayerHead is null!");
        } 
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(playerHead.transform);
    }
}
