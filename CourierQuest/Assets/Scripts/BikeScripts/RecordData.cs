using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using UnityEngine;
using Yarn.Unity;

public class RecordData : MonoBehaviour
{
    public BikeController bikeController;
    public DialogueRunner dialogueRunner;
    public string playerId = "";
    public int playerAge = -1;
    public double playerMaxHR = 0;
    [SerializeField] private float recordDataTime = 5.0f;
    private float timer = 0f;
    private int redCount = 0;
    private TextWriter tw;
    private string bikeDataFilename = "";
    private string storyDataFilename = "";
    
    
    // This function is called in the Awake() of the PlayerViewObjects game objbect
    public void SetUpRecording()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + $"/{playerId}_Data/");
        string dirName = Application.streamingAssetsPath + $"/{playerId}_Data/";

        dialogueRunner = GetComponent<PlayerViewObjects>().GetDialogueRunner();
        playerMaxHR = 208 - (0.7 * playerAge);
        
        // Set up files
        // Bike Data
        bikeDataFilename = CreateDataTextFile(dirName, $"{playerId}_playerData", $"----Player Bike Data: ID {playerId}, HRmax: {playerMaxHR}----\n");
        CreateCSV(bikeDataFilename + ".csv", "Timestamp, RPM, HR, Speed");

        // Story Data and Timed Data
        storyDataFilename = CreateDataTextFile(dirName, $"{playerId}_gameData", $"----Game Story Data: ID {playerId}----\n");
        CreateCSV(storyDataFilename + ".csv", "Timestamp, Node, Time Taken");
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

    public string CreateDataTextFile(string dirName, string fileName, string topLine)
    {
        string newFilename = dirName + fileName;

        if (!File.Exists(newFilename))
        {
            File.WriteAllText(newFilename + ".txt", topLine);
        }
        else
        {
            File.Delete(newFilename + ".txt");
            File.WriteAllText(newFilename + ".txt", topLine);
        }

        return newFilename;
    }

    // This function writes the data from the bike controller class into a txt file
    public void WriteDataString()
    {
        // read from bikeController
        string dataString = $"DateTime: {DateTime.Now}, RPM: {bikeController.RPM}, HR: {bikeController.heartRate}, Speed: {bikeController.speed}\n";
        List<string> dataList = new List<string>
        {
            $"{DateTime.Now}",
            $"{bikeController.RPM}",
            $"{bikeController.heartRate}",
            $"{bikeController.speed}"
        };

        // write to txt file
        File.AppendAllText(bikeDataFilename + ".txt", dataString);

        // write to CSV file
        WriteCSV(bikeDataFilename + ".csv", dataList);
    }

    // This function writes the data from the dialogue runner class into a txt file. (Called within the inspector in DialogueRunner.Events)
    public void WriteStoryDataString()
    {   
        // read from dialoguerunner
        string currentNodeString = $"DateTime: {DateTime.Now}, Current Node: {dialogueRunner.CurrentNodeName}\n";
        List<string> dataList = new List<string>
        {
            $"{DateTime.Now}",
            $"{dialogueRunner.CurrentNodeName}"
        };

        // write to txt file
        File.AppendAllText(storyDataFilename + ".txt", currentNodeString);

        // write to CSV file
        WriteCSV(storyDataFilename + ".csv", dataList);
    }

    // This function records the time it takes for the player to choose the option they want
    public void RecordTimer(float totalTime)
    {
        // read from dialoguerunner
        string timedChoiceString = $"DateTime: {DateTime.Now}, Current Node: {dialogueRunner.CurrentNodeName}, Timed Choice: {totalTime}\n";
        List<string> dataList = new List<string>
        {
            $"{DateTime.Now}",
            $"{dialogueRunner.CurrentNodeName}",
            $"{totalTime}"
        };
        
        // write to file
        File.AppendAllText(storyDataFilename + ".txt", timedChoiceString);

        // write to CSV file
        WriteCSV(storyDataFilename + ".csv", dataList);
    }

    // This function records the number of obstacles hit during the obstacle game
    public void RecordObstaclesHit(int obstaclesHit)
    {
        // read from dialoguerunner
        string obstaclesString = $"DateTime: {DateTime.Now}, Current Node: Start_your_journey, Obstacles Hit: {obstaclesHit}\n";

        // write to file
        File.AppendAllText(storyDataFilename + ".txt", obstaclesString);
    }

    // This function records the variables chosen throughout the game
    public void RecordVariables()
    {
        RecordRedCount();

        Dictionary<string, string> stringVariables = dialogueRunner.VariableStorage.GetAllVariables().StringVariables;
        string stringVarString = string.Join(Environment.NewLine, stringVariables);

        Dictionary<string, bool> boolVariables = dialogueRunner.VariableStorage.GetAllVariables().BoolVariables;
        string boolVarString = string.Join(Environment.NewLine, boolVariables);

        Dictionary<string, float> floatVariables = dialogueRunner.VariableStorage.GetAllVariables().FloatVariables;
        string floatVarString = string.Join(Environment.NewLine, floatVariables);
        
        // read from dialoguerunner
        string currentNodeString = $"String Variables: \n{stringVarString}\n\nBool Variables: \n{boolVarString}\n\nFloat Variables: \n{floatVarString}";

        // write to file
        File.AppendAllText(storyDataFilename + ".txt", currentNodeString);
    }

    // This function adds a count to the numebr of times the player has had the crystals at red
    public void CountRed()
    {
        redCount += 1;
    }

    // This function records the number of times that the player had the crystals turn red.
    public void RecordRedCount()
    {
        // read from dialoguerunner
        string currentNodeString = $"No. of times player hit red: {redCount}\n";

        // write to file
        File.AppendAllText(storyDataFilename + ".txt", currentNodeString);
    }

    // This function creates a csv file for record keeping.
    public void CreateCSV(string filename, string columnNames)
    {  
        tw = new StreamWriter(filename, false);
        tw.WriteLine(columnNames);
        tw.Close();
    }

    // This function writes a csv file for record keeping.
    public void WriteCSV(string filename, List<string> dataList)
    {
        string dataString = "";
        for (int i = 0; i < dataList.Count; i++)
        {
            if (i == dataList.Count-1)
                dataString = dataString + $"{dataList[i]}";
            else
                dataString = dataString + $"{dataList[i]},";
        }
        
        tw = new StreamWriter(filename, true);
        tw.WriteLine(dataString);
        tw.Close();
    }
}
