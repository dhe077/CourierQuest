using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;
using UnityEngine.SceneManagement;
using Dreamteck.Splines;

public class StoryCommands : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public CustomOptionsListView customOptionsListView;
    public Slider timerBar;
    public CanvasGroup sliderCanvasGroup;
    private OptionView timeoutOption = null;

    private PlayerViewManager playerViewManager;
    private EnvironmentManager environmentManager;
    private ExerciseQuestManager exerciseQuestManager;
    private AnimationManager animationManager;
    private AudioManager audioManager;

    private float timer = 0f;
    private float endTimer;
    private bool startTicking = false;
    private bool updateTimerBar = false;
    private float fadeSpeed = 1.0f;
    private float alpha = 0.196f;

    private string nextNodeName;

    public void Awake()
    {
        // Create a new command called 'camera_look', which looks at a target. 
        // Note how we're listing 'GameObject' as the parameter type.
        // dialogueRunner.AddCommandHandler<GameObject>(
        //     "camera_look",     // the name of the command
        //     CameraLookAtTarget // the method to run
        // );

        dialogueRunner.AddCommandHandler<float>(
            "startTimer",     // the name of the command
            StartTimer // the method to run
        );

        dialogueRunner.AddCommandHandler<string>(
            "interrruptTimer",     // the name of the command
            InterruptTimer // the method to run
        );

        dialogueRunner.AddCommandHandler<string>(
            "goToScene",
            ToStoryScene
        );

        dialogueRunner.AddCommandHandler<string>(
            "nextPosition",
            NextPosition
        );

        dialogueRunner.AddCommandHandler<string>(
            "specificPosition",
            SpecificPosition
        );

        // Courier Guildhall
        dialogueRunner.AddCommandHandler<string>(
            "spawnPackages",
            SpawnPackages
        );

        // Outrun Creature
        dialogueRunner.AddCommandHandler<string>(
            "startChase",
            StartChase
        );
        dialogueRunner.AddCommandHandler<bool>(
            "moveClose",
            MoveClose
        );
        dialogueRunner.AddCommandHandler<string>(
            "lightning",
            MakeLightning
        );
        dialogueRunner.AddCommandHandler<bool>(
            "rain",
            MakeRain
        );
        dialogueRunner.AddCommandHandler<string>(
            "prepareExerciseQuest",
            PrepareExerciseQuest
        );
        dialogueRunner.AddCommandHandler<string>(
            "startExerciseQuest",
            StartExerciseQuest
        );
        dialogueRunner.AddCommandHandler<string, bool>(
            "enable",
            EnableObject
        );
        dialogueRunner.AddCommandHandler<string, string>(
            "enableDisable",
            EnableAndDisableObjects
        );
        dialogueRunner.AddCommandHandler<bool>(
            "rideHorse",
            RideHorse
        );
        dialogueRunner.AddCommandHandler<string>(
            "setNextNodeName",
            SetNextNodeName
        );
        dialogueRunner.AddCommandHandler<string, string>(
            "animate",
            Animate
        );
        dialogueRunner.AddCommandHandler<string, float>(
            "playSound",
            PlaySound
        );
        dialogueRunner.AddCommandHandler<string, string, float>(
            "playSoundFrom",
            PlaySoundFrom
        );
        dialogueRunner.AddCommandHandler<string, float>(
            "setBackgroundMusic",
            SetBackgroundMusic
        );
    }

    public void Start()
    {
        // Start the slider invisible
        sliderCanvasGroup.alpha = 0;
    }
    
    public void Update()
    {
        // Timer
        if (startTicking == true) 
        {
            timer += Time.deltaTime;
            // Get the last option to set it's background for timeout
            int numOfOptions = customOptionsListView.numberOfOptions;
            if (numOfOptions > 0) 
            {
                timeoutOption = customOptionsListView.optionViews[numOfOptions-1];
                FadeInColor();
            }
            if (updateTimerBar == true)
                timerBar.value = timer;
            if (timer >= endTimer)
            {
                // Tell the timer to stop counting.
                startTicking = false;

                // Invoke the timeout option.
                TimerEnd();
            }
        }
    }

    // Yarn Commands //
    public void StartTimer(float end) 
    {
        // Reset the timer.
        timer = 0f;
        endTimer = end;
        startTicking = true;
        updateTimerBar = true;
        FadeInTimerBar();
    }

    public void InterruptTimer(string none)
    {
        startTicking = false;
        updateTimerBar = false;
        
        StopAllCoroutines();
        FadeOutTimerBar();
        FadeOutColor();
    }

    // This command is used to transition to the next scene
    public void ToStoryScene(string sceneName)
    {
        SceneChanger.Instance.ChangeScene(sceneName);
    }

    // This command moves the player to the next position in the SAME scene
    public void NextPosition(string none)
    {
        playerViewManager = GameObject.Find("PlayerViewManager").GetComponent<PlayerViewManager>();
        playerViewManager.NextPosition();
    }

    // This command moves the player to a specific position in the SAME scene
    public void SpecificPosition(string splineName)
    {
        playerViewManager = GameObject.Find("PlayerViewManager").GetComponent<PlayerViewManager>();
        playerViewManager.SpecificPosition(splineName);
    }

    // This command spawns the packages in the CourierGuildhall
    public void SpawnPackages(string none)
    {
        environmentManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        environmentManager.SpawnPackageObjects();
    }

    // These command starts makes the goblin chase the player in the Outrun Goblin scene
    public void StartChase(string none)
    {
        environmentManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        environmentManager.StartChasing();
    }
    public void MoveClose(bool close)
    {
        environmentManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        environmentManager.SetAtPlayerSide(close);
    }

    // This command flashes a light to simulate lightning
    public void MakeLightning(string none)
    {
        environmentManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        environmentManager.MakeLightning();
    }

    // This command flashes a light to simulate lightning
    public void MakeRain(bool rainSwitch)
    {
        environmentManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        environmentManager.MakeRain(rainSwitch);
    }

    // This command starts the corresponding exercise quest
    public void PrepareExerciseQuest(string questName)
    {
        exerciseQuestManager = GameObject.Find("ExerciseQuestManager").GetComponent<ExerciseQuestManager>();
        exerciseQuestManager.PrepareExerciseQuest(questName);
    }

    // This command starts the corresponding exercise quest
    public void StartExerciseQuest(string questName)
    {
        exerciseQuestManager = GameObject.Find("ExerciseQuestManager").GetComponent<ExerciseQuestManager>();
        exerciseQuestManager.StartExerciseQuest(questName);
    }

    // This command enables an object
    public void EnableObject(string enableObj, bool enableBool)
    {
        environmentManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        environmentManager.EnableObject(enableObj, enableBool);
    }

    // This command enables 1 object and disables another object
    public void EnableAndDisableObjects(string enableObj, string disableObj)
    {
        environmentManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        environmentManager.EnableAndDisableObjects(enableObj, disableObj);
    }

    // This command enables the story to make the player hop on and off the horse.
    public void RideHorse(bool hopOn)
    {
        playerViewManager = GameObject.Find("PlayerViewManager").GetComponent<PlayerViewManager>();
        playerViewManager.RideHorse(hopOn);
    }

    // This command sets the name of the next node
    public void SetNextNodeName(string nodeName)
    {
        playerViewManager = GameObject.Find("PlayerViewManager").GetComponent<PlayerViewManager>();
        playerViewManager.SetSceneToChangeTo(nodeName);
    }

    // This command plays an animation from the animation object using the objects name and an animation name
    public void Animate(string objectName, string animationName)
    {
        animationManager = GameObject.Find("AnimationManager").GetComponent<AnimationManager>();
        animationManager.PlayAnimation(objectName, animationName);
    }

    // This command plays an audio clip from the list in the AudioManager object using its audio clip name
    public void PlaySound(string soundName, float volume)
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlaySound(soundName, volume);
    }

    // This command plays an audio clip from the list in the AudioManager object using its audio clip name
    public void PlaySoundFrom(string objectName, string soundName, float volume)
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.PlaySoundFrom(objectName, soundName, volume);
    }

    // This command plays an audio clip from the list in the AudioManager object using its audio clip name
    public void SetBackgroundMusic(string soundName, float volume)
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.SetBackgroundMusic(soundName, volume);
    }




    // Functions //
    public void TimerEnd()
    {
        int numOfOptions = customOptionsListView.numberOfOptions;
        OptionView lastOptionView = customOptionsListView.optionViews[numOfOptions-1];
        lastOptionView.InvokeOptionSelected();
        
        StopAllCoroutines();
        FadeOutTimerBar();
        FadeOutColor();
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            sliderCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        sliderCanvasGroup.alpha = endAlpha;
    }

    public void FadeInTimerBar()
    {
        // Reset the timer bar.
        timerBar.value = timerBar.minValue;
        // Set the timer bar maximum.
        timerBar.maxValue = endTimer;
        // Gradually increase the alpha value to fade in
        StartCoroutine(Fade(0, 1, fadeSpeed));
    }

    public void FadeOutTimerBar()
    {
        // Gradually decrease the alpha value to fade out
        StartCoroutine(Fade(1, 0, fadeSpeed));
    }

    private IEnumerator FadeColor(float startColor, float endColor, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            // Increase the RED value of the color
            timeoutOption.transform.GetChild(0).GetComponent<Image>().color = new Color(Mathf.Lerp(startColor, endColor, elapsedTime / duration), 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        timeoutOption.transform.GetChild(0).GetComponent<Image>().color = new Color(endColor, 0, 0, alpha);
    }

    public void FadeOutColor()
    {
        // Gradually decrease the color value to fade out
        StartCoroutine(FadeColor(1, 0, endTimer/2));
    }

    public void FadeInColor()
    {
        // Gradually decrease the color value to fade out
        StartCoroutine(FadeColor(0, 1, endTimer));
    }

    public void ChooseOption(int optionIndex)
    {
        // Get the chosen option
        OptionView chosenOptionView = customOptionsListView.optionViews[optionIndex];

        // Choose that option
        chosenOptionView.InvokeOptionSelected();
        
        //StopAllCoroutines();
    }

    public void StartFrom(string nodeName)
    {
        dialogueRunner.StartDialogue(nodeName);
    }

    public bool StillRunning()
    {
        return dialogueRunner.IsDialogueRunning;
    }

}
