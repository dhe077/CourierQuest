using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseQuestManager : MonoBehaviour
{
    [Header("----Player View Object----")]
    [SerializeField] private GameObject playerView;

    public bool startQuest = false;

    void Start()
    {
        playerView = GameObject.Find("PlayerView");
    }

    public GameObject GetPlayerView()
    {
        return playerView;
    }

    public void SetStartQuest(bool start) { startQuest = start; }

    public bool GetStartQuest() { return startQuest; }



}
