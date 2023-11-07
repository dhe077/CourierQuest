using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCounter : MonoBehaviour
{
    [SerializeField] private ObstacleGame obstacleGame;

    private void Start()
    {
        obstacleGame = GameObject.Find("ObstacleGameSystem").GetComponent<ObstacleGame>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            //Debug.Log($"Hit {other.name}!");
            obstacleGame.HitObstacle();
        }
    }   
}
