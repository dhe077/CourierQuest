using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SceneSelector : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public InMemoryVariableStorage variables;
    
    public void JumpToBjornScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$firstPackage", "large");
        dialogueRunner.StartDialogue("Arrive_at_your_first_destination");
    }

    public void JumpToPetuniaScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$firstPackage", "small");
        dialogueRunner.StartDialogue("Arrive_at_your_first_destination");
    }

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
        variables.SetValue("$diamond", true);
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
        variables.SetValue("$path", "left");
        dialogueRunner.StartDialogue("Arrive_at_your_third_destination");
    }

    public void JumpToThakaliScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "green");
        variables.SetValue("$path", "left");
        dialogueRunner.StartDialogue("Arrive_at_your_third_destination");
    }

    public void JumpToDaisyScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "blue");
        variables.SetValue("$path", "left");
        dialogueRunner.StartDialogue("Arrive_at_your_third_destination");
    }

    public void JumpToValScene()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "yellow");
        variables.SetValue("$path", "left");
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

    public void JumpToLeytonExQuest()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "red");
        variables.SetValue("$path", "left");
        dialogueRunner.StartDialogue("Ask_how_you_can_help_heat_up_the_coals");
    }

    public void JumpToThakaliExQuest()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "green");
        variables.SetValue("$path", "left");
        dialogueRunner.StartDialogue("Offer_to_use_Stormhoof_to_pull_the_plow");
    }

    public void JumpToDaisyExQuest()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "blue");
        variables.SetValue("$path", "left");
        dialogueRunner.StartDialogue("Chase_the_fairy");
    }

    public void JumpToValExQuest()
    {
        dialogueRunner.Stop();
        variables.SetValue("$thirdPackage", "yellow");
        variables.SetValue("$path", "left");
        dialogueRunner.StartDialogue("Escape_the_shooters");
    }

    // Endings

    public void JumpToEldoriaTrue()
    {
        dialogueRunner.Stop();
        variables.SetValue("$specialPackage", true);
        variables.SetValue("$muscleSpirit", true);
        dialogueRunner.StartDialogue("Ride_towards_Eldoria");
    }

    public void JumpToEldoriaFalse()
    {
        dialogueRunner.Stop();
        variables.SetValue("$specialPackage", false);
        variables.SetValue("$muscleSpirit", true);
        dialogueRunner.StartDialogue("Ride_towards_Eldoria");
    }

    public void JumpToVolcanoTrue()
    {
        dialogueRunner.Stop();
        variables.SetValue("$specialPackage", true);
        variables.SetValue("$muscleSpirit", true);
        variables.SetValue("$dagger", true);
        dialogueRunner.StartDialogue("Ride_towards_the_volcano");
    }

    public void JumpToVolcanoFalse()
    {
        dialogueRunner.Stop();
        variables.SetValue("$specialPackage", false);
        variables.SetValue("$muscleSpirit", true);
        dialogueRunner.StartDialogue("Ride_towards_the_volcano");
    }

    public void JumpToWhistleTrue()
    {
        dialogueRunner.Stop();
        variables.SetValue("$specialPackage", true);
        variables.SetValue("$whistle", true);

        //variables.SetValue("$metBjorn", true);
        variables.SetValue("$metPetunia", true);

        //variables.SetValue("$metMudbrick", true);
        //variables.SetValue("$metOli", true);
        variables.SetValue("$metFinn", true);

        //variables.SetValue("$metLeyton", true);
        //variables.SetValue("$metThakali", true);
        //variables.SetValue("$metDaisy", true);
        variables.SetValue("$metVal", true);
        dialogueRunner.StartDialogue("Ride_towards_the_volcano");
    }

    public void JumpToRoadside()
    {
        dialogueRunner.Stop();
        variables.SetValue("$specialPackage", true);
        dialogueRunner.StartDialogue("Wake_up_near_the_road");
    }

    public void JumpToBushesHut()
    {
        dialogueRunner.Stop();
        variables.SetValue("$followedPath", true);
        dialogueRunner.StartDialogue("Wake_up_in_the_bushes");
    }

    public void JumpToBushesRoad()
    {
        dialogueRunner.Stop();
        variables.SetValue("$followedPath", false);
        dialogueRunner.StartDialogue("Wake_up_in_the_bushes");
    }

    public void JumpToCustom(string nodeName)
    {
        dialogueRunner.Stop();
        variables.SetValue("$metVal", true);
        //variables.SetValue("$metDaisy", true);
        //variables.SetValue("$metThakali", true);
        //variables.SetValue("$metLeyton", true);
        
        //variables.SetValue("$metFinn", true);
        variables.SetValue("$metOli", true);
        //variables.SetValue("$metMudbrick", true);

        //variables.SetValue("$metBjorn", true);
        variables.SetValue("$metPetunia", true);

        //variables.SetValue("$specialPackage", true);

        variables.SetValue("$muscleSpirit", true);
        //variables.SetValue("$pattersbyOriginal", true);

        //variables.SetValue("$diamond", true);
        variables.SetValue("$animalPendant", true);
        //variables.SetValue("$smokeVial", true);

        //variables.SetValue("$dagger", true);
        variables.SetValue("$axe", true);
        //variables.SetValue("$lantern", true);
        //variables.SetValue("$magicalCompass", true);
        //variables.SetValue("$assassin", true);

        dialogueRunner.StartDialogue(nodeName);
    }
}
