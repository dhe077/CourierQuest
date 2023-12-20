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
}
