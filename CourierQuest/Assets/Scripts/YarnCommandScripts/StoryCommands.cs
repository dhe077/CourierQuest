using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class StoryCommands : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public CustomOptionsListView customOptionsListView;

    private float timer = 0f;
    private float endTimer = 5f;
    private bool startTicking = false;
    private string nextNode;

    public void Awake()
    {
        // Create a new command called 'camera_look', which looks at a target. 
        // Note how we're listing 'GameObject' as the parameter type.
        // dialogueRunner.AddCommandHandler<GameObject>(
        //     "camera_look",     // the name of the command
        //     CameraLookAtTarget // the method to run
        // );

        dialogueRunner.AddCommandHandler<string>(
            "startTimer",     // the name of the command
            StartTimer // the method to run
        );

        dialogueRunner.AddCommandHandler<string>(
            "interrruptTimer",     // the name of the command
            InterruptTimer // the method to run
        );
    }
    
    public void Update()
    {
        if (startTicking == true) 
        {
            timer += Time.deltaTime;
            if (timer >= endTimer)
            {
                startTicking = false;
                TimerEnd(nextNode);
                Debug.Log("Timer done.");
            }
        }
    }

    public void StartTimer(string node) 
    {
        // Reset the timer.
        timer = 0f;
        startTicking = true;
        nextNode = node;
        Debug.Log($"Start the timer for {endTimer} seconds.");
    }

    public void InterruptTimer(string node)
    {
        startTicking = false;
        Debug.Log("Stop the timer!");
    }

    public void TimerEnd(string node)
    {
        int numOfOptions = customOptionsListView.numberOfOptions;
        OptionView lastOptionView = customOptionsListView.optionViews[numOfOptions-1];
        lastOptionView.InvokeOptionSelected();
        lastOptionView.Option.IsAvailable = false;
        
        //dialogueRunner.Stop();
        //dialogueRunner.StartDialogue(nextNode);
        
    }

}
