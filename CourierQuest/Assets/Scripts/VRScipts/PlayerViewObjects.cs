using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PlayerViewObjects : MonoBehaviour
{
    public StoryCommands storyCommands;
    public DialogueRunner dialogueRunner;
    public GameObject mainCamera;
    public AudioSource backgroundMusic;
    public GameObject branchingStoryObject;
    public GameObject nonBranchingStoryObject;

    public bool playBranchingStory = true;


    public void Awake()
    {
        Debug.Log("Objects");
        if (playBranchingStory)
        {
            branchingStoryObject.SetActive(true);
            nonBranchingStoryObject.SetActive(false);
            storyCommands = branchingStoryObject.GetComponent<StoryCommands>();
            dialogueRunner = branchingStoryObject.GetComponent<DialogueRunner>();
        }
        else
        {
            nonBranchingStoryObject.SetActive(true);
            branchingStoryObject.SetActive(false);
            storyCommands = nonBranchingStoryObject.GetComponent<StoryCommands>();
            dialogueRunner = nonBranchingStoryObject.GetComponent<DialogueRunner>();
        }
        GetComponent<RecordData>().SetUpRecording();
    }


    public StoryCommands GetStoryCommands()
    {
        return storyCommands;
    }

    public GameObject GetMainCamera()
    {
        return mainCamera;
    }

    public AudioSource GetBackgroundMusic()
    {
        return backgroundMusic;
    }

    public DialogueRunner GetDialogueRunner()
    {
        return dialogueRunner;
    }

    public void TogglePlayBranching(Toggle toggleBool)
    {
        playBranchingStory = toggleBool.isOn;
    }
}
