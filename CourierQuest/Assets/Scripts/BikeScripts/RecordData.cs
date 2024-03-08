using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Yarn.Unity;

public class RecordData : MonoBehaviour
{
    public BikeController bikeController;
    public DialogueRunner dialogueRunner;

    private string bikeDataFileName = Application.streamingAssetsPath + "/BikeData/" + "playerData" + ".txt";
    private string gameDataFileName = Application.streamingAssetsPath + "/GameData/" + "storyData" + ".txt";
    private string timedDataFileName = Application.streamingAssetsPath + "/GameData/" + "timedChoiceData" + ".txt";
    private string dataString = "";

    public string playerId = "";

    [SerializeField] private float recordDataTime = 5.0f;
    private float timer = 0f;

    private int redCount = 0;
    
    public void SetUpRecording()
    {
        dialogueRunner = GetComponent<PlayerViewObjects>().GetDialogueRunner();
        CreatePlayerDataFile();
        CreateStoryDataFile();
        CreateTimedChoiceDataFile();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= recordDataTime)
        {
            WriteDataString();
            timer = 0f;
        }
        
    }

    public void CreatePlayerDataFile()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/BikeData/");

        string txtDocumentName = Application.streamingAssetsPath + "/BikeData/" + "playerData" + ".txt";

        if (!File.Exists(txtDocumentName))
        {
            File.WriteAllText(txtDocumentName, $"----Player Bike Data: ID {playerId}----\n");
        }
        else
        {
            File.Delete(txtDocumentName);
            File.WriteAllText(txtDocumentName, $"----Player Bike Data: ID {playerId}----\n");
        }
    }

    public void CreateStoryDataFile()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/GameData/");

        string txtDocumentName = Application.streamingAssetsPath + "/GameData/" + "storyData" + ".txt";

        if (!File.Exists(txtDocumentName))
        {
            File.WriteAllText(txtDocumentName, $"----Game Story Data: ID {playerId}----\n");
        }
        else
        {
            File.Delete(txtDocumentName);
            File.WriteAllText(txtDocumentName, $"----Game Story Data: ID {playerId}----\n");
        }
    }

    public void CreateTimedChoiceDataFile()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/GameData/");

        string txtDocumentName = Application.streamingAssetsPath + "/GameData/" + "timedChoiceData" + ".txt";

        if (!File.Exists(txtDocumentName))
        {
            File.WriteAllText(txtDocumentName, $"----Timed Choice Data: ID {playerId}----\n");
        }
        else
        {
            File.Delete(txtDocumentName);
            File.WriteAllText(txtDocumentName, $"----Timed Choice Data: ID {playerId}----\n");
        }
    }

    // This function writes the data from the bike controller class into a txt file
    public void WriteDataString()
    {
        // read from bikeController
        dataString = $"DateTime: {DateTime.Now}, RPM: {bikeController.RPM}, HR: {bikeController.heartRate}, Speed: {bikeController.speed}\n";

        // write to file
        File.AppendAllText(bikeDataFileName, dataString);
    }

    // This function writes the data from the dialogue runner class into a txt file
    public void WriteStoryDataString()
    {   
        // read from dialoguerunner
        string currentNodeString = $"DateTime: {DateTime.Now}, Current Node: {dialogueRunner.CurrentNodeName}\n";
        
        // write to file
        File.AppendAllText(gameDataFileName, currentNodeString);
    }

    // This function records the time it takes for the player to choose the option they want
    public void RecordTimer(float totalTime)
    {
        // read from dialoguerunner
        string timedChoiceString = $"DateTime: {DateTime.Now}, Current Node: {dialogueRunner.CurrentNodeName}, Timed Choice: {totalTime}\n";
        
        // write to file
        File.AppendAllText(timedDataFileName, timedChoiceString);
    }

    // This function records the number of obstacles hit during the obstacle game
    public void RecordObstaclesHit(int obstaclesHit)
    {
        // read from dialoguerunner
        string obstaclesString = $"DateTime: {DateTime.Now}, Current Node: Start_your_journey, Obstacles Hit: {obstaclesHit}\n";
        
        // write to file
        File.AppendAllText(gameDataFileName, obstaclesString);
    }

    // This function records the variables chosen throughout the game
    public void RecordVariables()
    {
        Dictionary<string, string> stringVariables = dialogueRunner.VariableStorage.GetAllVariables().StringVariables;
        string stringVarString = string.Join(Environment.NewLine, stringVariables);

        Dictionary<string, bool> boolVariables = dialogueRunner.VariableStorage.GetAllVariables().BoolVariables;
        string boolVarString = string.Join(Environment.NewLine, boolVariables);

        Dictionary<string, float> floatVariables = dialogueRunner.VariableStorage.GetAllVariables().FloatVariables;
        string floatVarString = string.Join(Environment.NewLine, boolVariables);
        
        // read from dialoguerunner
        string currentNodeString = $"String Variables: \n{stringVarString}\n\nBool Variables: \n{boolVarString}\n\nFloat Variables: \n{floatVarString}";

        // write to file
        File.AppendAllText(gameDataFileName, currentNodeString);
    }

    // This function adds a count to the numebr of times the player has had the crystals at red
    public void CountRed()
    {
        redCount += 1;
    }

    // This function records the number of times that the player had the crystals turn red
    public void RecordRedCount()
    {
        // read from dialoguerunner
        string currentNodeString = $"No. of times player hit red: {redCount}\n";

        // write to file
        File.AppendAllText(gameDataFileName, currentNodeString);
    }
}
