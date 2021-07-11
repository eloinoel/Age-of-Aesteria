using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;


    //singleton
    public static AudioManager instance;

    private Sound currectlyPlayingMusic;
    private float timeSinceScene;
    private string currentScene;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        //make audiomanager persist through scenes
        DontDestroyOnLoad(gameObject);
        currentScene = SceneManager.GetActiveScene().name;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.group;
        }
    }

    void Start()
    {
        Play("music_relaxed");
    }

    void Update()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        timeSinceScene += Time.deltaTime;

        //play next song
        if(timeSinceScene >= currectlyPlayingMusic.clip.length)
        {
            if (activeScene == "MainMenuScene")
            {
                string nextSong = "atgeir";
                if (currectlyPlayingMusic != null) {
                    if (currectlyPlayingMusic.name == "atgeir")
                    {
                        nextSong = "music_relaxed";
                    }
                    FadeOut(currectlyPlayingMusic.name); 
                }
                Play(nextSong);   
                
            }

            if(activeScene == "ForestScene")
            {
                string nextSong = "music_wDrums";
                if (currectlyPlayingMusic != null) {
                    if (currectlyPlayingMusic.name == "music_wDrums")
                    {
                        nextSong = "music_Fate";
                    }
                    FadeOut(currectlyPlayingMusic.name); 
                }
                Play(nextSong);
            }
            timeSinceScene = 0f;
        }

        
        //upon scene change
        if(activeScene != currentScene)
        {
            timeSinceScene = 0f;
            currentScene = activeScene;
            if (activeScene == "ForestScene")
            {
                if (currectlyPlayingMusic != null)
                {  
                    FadeOut(currectlyPlayingMusic.name);
                }
                Play("music_Fate");
            }
            if (activeScene == "MainMenuScene")
            {
                if (currectlyPlayingMusic != null)
                {
                    FadeOut(currectlyPlayingMusic.name);
                }                
                Play("music_relaxed");
            }
        }
    }



    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Play(): Sound " + name + " doesn't exist");
            return;
        }
        s.source.volume = 1;
        s.source.Play();
        currectlyPlayingMusic = s;
    }

    public void Stop(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Stop(): Sound " + name + " doesn't exist");
            return;
        }
        s.source.Stop();
        currectlyPlayingMusic = null;
    }

    public void FadeOut(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Stop(): Sound " + name + " doesn't exist");
            return;
        }
        //fade out volume
        StartCoroutine(StartFade(s.source, 1, 0));      
    }


    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.Stop();
        yield break;
    }
}
