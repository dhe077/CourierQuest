using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewObjects : MonoBehaviour
{
    public StoryCommands storyCommands;

    public StoryCommands GetStoryCommands()
    {
        return storyCommands;
    }
}
