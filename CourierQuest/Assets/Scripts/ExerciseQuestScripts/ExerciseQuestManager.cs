using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseQuestManager : MonoBehaviour
{
    [Header("----Player View Object----")]
    [SerializeField] private GameObject playerView;

    void Start()
    {
        playerView = GameObject.Find("PlayerView");
    }

    public GameObject GetPlayerView()
    {
        return playerView;
    }

    public void StartExerciseQuest(string questName)
    {
        switch (questName)
        {
            case "mudbrick":
                GetComponent<MudbrickExQuest>().StartLifting();
                break;
            case "oli":
                GetComponent<OliExQuest>().StartEscaping();
                break;
            case "finn":
                GetComponent<FinnExQuest>().StartGenerating();
                break;
            case "leyton":
                GetComponent<LeytonExQuest>().StartHeating();
                break;
            case "thakali":
                GetComponent<ThakaliExQuest>().StartPlowing();
                break;
        }
    }

    public void PrepareExerciseQuest(string questName)
    {
        switch (questName)
        {
            case "mudbrick":
                GetComponent<MudbrickExQuest>().PreparePlayer();
                break;
            case "oli":
                GetComponent<OliExQuest>().PreparePlayer();
                break;
            case "finn":
                GetComponent<FinnExQuest>().PreparePlayer();
                break;
            case "leyton":
                GetComponent<LeytonExQuest>().PreparePlayer();
                break;
            case "thakali":
                GetComponent<ThakaliExQuest>().PreparePlayer();
                break;
            // case "finn":
            //     GetComponent<FinnExQuest>().PreparePlayer();
            //     break;
        }
    }



}
