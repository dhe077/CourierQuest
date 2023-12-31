using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SceneSelector : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public InMemoryVariableStorage variables;
    // Start is called before the first frame update
    public void JumpToMudbrickScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$secondPackage", "round");
        dialogueRunner.StartDialogue("Arrive_at_your_second_destination");
    }

    public void JumpToOliScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$secondPackage", "square");
        dialogueRunner.StartDialogue("Arrive_at_your_second_destination");
    }

    public void JumpToFinnScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$secondPackage", "triangle");
        dialogueRunner.StartDialogue("Arrive_at_your_second_destination");
    }

    public void JumpToLeytonScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "red");
        dialogueRunner.StartDialogue("Arrive_at_your_third_destination");
    }

    public void JumpToThakaliScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "green");
        dialogueRunner.StartDialogue("Arrive_at_your_third_destination");
    }

    public void JumpToDaisyScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "blue");
        dialogueRunner.StartDialogue("Arrive_at_your_third_destination");
    }

    public void JumpToValScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "yellow");
        dialogueRunner.StartDialogue("Arrive_at_your_third_destination");
    }

    public void JumpToFollowPathLeft()
    {
        dialogueRunner.Stop();
        variables.SetValue("$followedPath", true);
        dialogueRunner.StartDialogue("Do_nothing");
    }

    public void JumpToNotFollowPathLeft()
    {
        dialogueRunner.Stop();
        variables.SetValue("$followedPath", false);
        dialogueRunner.StartDialogue("Do_nothing");
    }

    public void JumpToDaisyExQuest()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "blue");
        dialogueRunner.StartDialogue("Chase_the_fairy");
    }
}
