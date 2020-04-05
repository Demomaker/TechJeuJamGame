using UnityEngine;

public class Sound  
{
    public AudioClip AudioClip;
    public GameObject Origin;

    public Sound(AudioClip audioClip, GameObject origin)
    {
        AudioClip = audioClip;
        Origin = origin;
    }
}