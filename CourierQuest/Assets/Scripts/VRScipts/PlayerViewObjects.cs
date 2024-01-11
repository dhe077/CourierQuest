using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewObjects : MonoBehaviour
{
    public StoryCommands storyCommands;
    public GameObject mainCamera;

    public StoryCommands GetStoryCommands()
    {
        return storyCommands;
    }

    public GameObject GetMainCamera()
    {
        return mainCamera;
    }
}
