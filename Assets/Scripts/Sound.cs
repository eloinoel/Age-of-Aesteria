using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public AudioMixerGroup group;
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(.1f, 3)]
    public float pitch = 1f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}


