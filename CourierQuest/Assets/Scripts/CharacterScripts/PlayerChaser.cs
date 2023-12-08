using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
    private GameObject playerView;
    private Vector3 playerPos;
    private bool startChasing = false;
    private GameObject chaserObject;
    private Vector3 targetPosition;
    private float moveSpeed = 20f;


    public bool atPlayerSide = true;
    [SerializeField] private float yVal = 2;
    [SerializeField] private int playerCloseOffset = 2;
    [SerializeField] private int playerFarOffset = 3;

    private List<Vector3> positions;
    private int positionIndex;

    private void Start()
    {
        // Initialize the list with zero vectors
        Vector3 x = Vector3.zero;
        positions = new List<Vector3>{x, x, x, x, x, x, x, x};
    }

    // Update is called once per frame
    void Update()
    {
        if (startChasing == true)
        {
            playerPos = playerView.transform.position;

            // Keep setting the discrete positions
            SetPositions();
            
            if (atPlayerSide == true)
                BesidePlayer();
            else if (atPlayerSide == false)
                FollowPlayer();
        }
    }

    public void StartChasing(GameObject pv, GameObject chaser)
    {
        playerView = pv;

        // Spawn the object and move it to the player
        playerPos = playerView.transform.position;
        Vector3 spawnPos;
        spawnPos = new Vector3(playerPos.x, yVal, playerPos.z - 5);
        chaserObject = Instantiate(chaser, spawnPos, Quaternion.identity);

        // Start InvokeRepeating functions
        StartInvokeRepeating();

        // Begin the chase
        startChasing = true;
    }

    public void BesidePlayer()
    {
        if (playerPos.x > 0) // Player is on the right
            targetPosition = new Vector3(playerPos.x - playerCloseOffset, yVal, playerPos.z + playerCloseOffset);
        else
            targetPosition = new Vector3(playerPos.x + playerCloseOffset, yVal, playerPos.z + playerCloseOffset);
        chaserObject.transform.position = Vector3.Lerp(chaserObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void FollowPlayer()
    {
        targetPosition = positions[positionIndex];
        chaserObject.transform.position = Vector3.Lerp(chaserObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void RandomPosition()
    {
        System.Random rnd = new System.Random();
        int newPosIndex = rnd.Next(0, 8);
        positionIndex = newPosIndex;
    }

    private void SetPositions()
    {
        // RIght side
        positions[0] = new Vector3(playerPos.x - playerFarOffset, yVal, playerPos.z + playerFarOffset);
        positions[1] = new Vector3(playerPos.x - (playerFarOffset * 2), yVal, playerPos.z + playerFarOffset);
        positions[2] = new Vector3(playerPos.x - (playerFarOffset * 2), yVal, playerPos.z + (playerFarOffset * 2));
        positions[3] = new Vector3(playerPos.x - playerFarOffset, yVal, playerPos.z + (playerFarOffset * 2));

        // Left side
        positions[4] = new Vector3(playerPos.x + playerFarOffset, yVal, playerPos.z + playerFarOffset);
        positions[5] = new Vector3(playerPos.x + (playerFarOffset * 2), yVal, playerPos.z + playerFarOffset);
        positions[6] = new Vector3(playerPos.x + (playerFarOffset * 2), yVal, playerPos.z + (playerFarOffset * 2));
        positions[7] = new Vector3(playerPos.x + playerFarOffset, yVal, playerPos.z + (playerFarOffset * 2));
    }

    private void StartInvokeRepeating()
    {
        InvokeRepeating("RandomPosition", 0f, 10f);
        InvokeRepeating("ReduceMovementSpeed", 0f, 10f);
    }

    private void ReduceMovementSpeed()
    {
        moveSpeed -= 1;
    }

    public void SetAtPlayerSide(bool x)
    {
        atPlayerSide = x;
    }
}
