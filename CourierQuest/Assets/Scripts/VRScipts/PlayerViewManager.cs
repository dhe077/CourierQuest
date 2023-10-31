using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewManager : MonoBehaviour
{
    [SerializeField] private GameObject playerView;
    [SerializeField] public Transform startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerView = GameObject.Find("PlayerView");

        SetTransform();
    }

    private void SetTransform()
    {
        playerView.transform.position = startingPosition.position;
        playerView.transform.rotation = startingPosition.rotation;
    }
}
