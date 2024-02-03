using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----Player View Object----")]
    [SerializeField] private GameObject playerView;

    [Header("----Environment Manager----")]
    [SerializeField] private EnvironmentManager environmentManager;

    [Header("----Audio Objects----")]
    public AudioSource playerBackgroundMusic;
    public AudioSource playableAudioSource;
    public List<AudioClip> audioSources;

    public void Start()
    {
        playerView = GameObject.Find("PlayerView");
        playerBackgroundMusic = playerView.GetComponent<PlayerViewObjects>().GetBackgroundMusic();
    }

    public void PlaySound(string soundName, float volume)
    {
        if (soundName == "Stop")
            playableAudioSource.Stop();
        else
        {
            for (int i = 0; i < audioSources.Count; i++)
            {
                if (audioSources[i].name == soundName)
                {
                    playableAudioSource.PlayOneShot(audioSources[i], volume);
                }
            }
        }
    }

    public void PlaySoundFrom(string objectName, string soundName, float volume)
    {
        environmentManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        GameObject gameObject = environmentManager.FindObjectInList(objectName);

        if (soundName == "Stop")
            playableAudioSource.Stop();
        else
        {
            for (int i = 0; i < audioSources.Count; i++)
            {
                if (audioSources[i].name == soundName)
                {
                    AudioSource soundSource = gameObject.GetComponent<AudioSource>();
                    if (soundSource != null)
                    {
                        soundSource.PlayOneShot(audioSources[i], volume);
                    }
                    else
                    {
                        Debug.Log($"Object {objectName} does not have an AudioSource component.");
                    }
                    
                }
            }
        }
    }

    public void SetBackgroundMusic(string soundName, float volume)
    {
        if (soundName == "Stop")
            playerBackgroundMusic.Stop();
        else
        {
            for (int i = 0; i < audioSources.Count; i++)
            {
                if (audioSources[i].name == soundName)
                {
                    playerBackgroundMusic.clip = audioSources[i];
                    playerBackgroundMusic.volume = volume;
                    playerBackgroundMusic.Play();
                }
            }
        }
    }

    public void TriggerBackgroundMusic(string soundName)
    {
        SetBackgroundMusic(soundName, 0.5f);
    }

    public void TriggerSound(string soundName)
    {
        PlaySound(soundName, 0.5f);
    }
}
