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
    private string dataString = "";

    [SerializeField] private float recordDataTime = 5.0f;
    private float timer = 0f;

    // Start is called before the first frame update
    
    void Awake()
    {
        CreatePlayerDataFile();
        CreateStoryDataFile();
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

    // This function writes the data from the bike controller class into a txt file
    public void WriteDataString()
    {
        // read from bikeController
        dataString = $"DateTime: {DateTime.Now}, RPM: {bikeController.RPM}, HR: {bikeController.heartRate}\n";

        // write to file
        File.AppendAllText(bikeDataFileName, dataString);
    }

    public void WriteStoryDataString()
    {   
        // read from dialoguerunner
        string currentNodeString = $"DateTime: {DateTime.Now}, Current Node: {dialogueRunner.CurrentNodeName}\n";
        
        // write to file
        File.AppendAllText(gameDataFileName, currentNodeString);
    }

    public void CreatePlayerDataFile()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/BikeData/");

        string txtDocumentName = Application.streamingAssetsPath + "/BikeData/" + "playerData" + ".txt";

        if (!File.Exists(txtDocumentName))
        {
            File.WriteAllText(txtDocumentName, "----Player Bike Data----\n");
        }
        else
        {
            File.Delete(txtDocumentName);
            File.WriteAllText(txtDocumentName, "----Player Bike Data----\n");
        }
    }

    public void CreateStoryDataFile()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/GameData/");

        string txtDocumentName = Application.streamingAssetsPath + "/GameData/" + "storyData" + ".txt";

        if (!File.Exists(txtDocumentName))
        {
            File.WriteAllText(txtDocumentName, "----Game Story Data----\n");
        }
        else
        {
            File.Delete(txtDocumentName);
            File.WriteAllText(txtDocumentName, "----Game Story Data----\n");
        }
    }
}
