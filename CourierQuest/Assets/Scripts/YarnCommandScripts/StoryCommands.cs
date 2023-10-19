using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;

public class StoryCommands : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public CustomOptionsListView customOptionsListView;
    public Slider timerBar;
    public CanvasGroup sliderCanvasGroup;
    private OptionView timeoutOption;

    private float timer = 0f;
    private float endTimer;
    private bool startTicking = false;
    private bool updateTimerBar = false;
    private float fadeSpeed = 1.0f;
    private float alpha = 0.196f;

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
                StartCoroutine(FadeColor(0, 1, endTimer));
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
        FadeOutTimerBar();
    }




    // Functions //
    public void TimerEnd()
    {
        int numOfOptions = customOptionsListView.numberOfOptions;
        OptionView lastOptionView = customOptionsListView.optionViews[numOfOptions-1];
        lastOptionView.InvokeOptionSelected();
        FadeOutTimerBar();
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
            timeoutOption.transform.GetChild(0).GetComponent<Image>().color = new Color(Mathf.Lerp(startColor, endColor, elapsedTime / duration), 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        timeoutOption.transform.GetChild(0).GetComponent<Image>().color = new Color(endColor, 0, 0, alpha);
    }
}
