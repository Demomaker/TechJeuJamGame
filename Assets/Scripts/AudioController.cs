using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour 
{
    private Coroutine soundCoroutine;
    private Queue<Sound> soundQueue = new Queue<Sound>();
    private Sound currentSound = null;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundAudioSource;

    public float MusicVolume => musicAudioSource.volume;
    public float SoundVolume => soundAudioSource.volume;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        musicAudioSource.Play();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        Finder.OnMusicVolumeChanged.Notify += ChangeMusicVolume;
        Finder.OnSoundVolumeChanged.Notify += ChangeSoundVolume;
        Finder.OnSoundPlayEventChannel.Notify += PlaySound;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        Finder.OnMusicVolumeChanged.Notify -= ChangeMusicVolume;
        Finder.OnSoundVolumeChanged.Notify -= ChangeSoundVolume;
        Finder.OnSoundPlayEventChannel.Notify -= PlaySound;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        if(soundCoroutine != null)
        StopCoroutine(soundCoroutine);
    }

    public void ChangeMusicVolume(float volume) 
    {
        musicAudioSource.volume = volume;
    }

    public void ChangeSoundVolume(float volume) 
    {
        soundAudioSource.volume = volume;
    }

    public void PlaySound(GameObject sourceObject, AudioClip soundToPlay) 
    {
        QueueSound(new Sound(soundToPlay, sourceObject));
        /*bool availableAudioSourceFound = false;
        for(int i = 0; i < soundAudioSources.Count; i++) 
        {
            if(!soundAudioSources[i].isPlaying) 
            {
                if(!availableAudioSourceFound) 
                {
                    availableAudioSourceFound = true;
                    soundAudioSources[i].clip = soundToPlay;
                }
                else 
                {
                    soundAudioSources.RemoveAt(i);
                    Destroy(soundAudioSources[i]);
                }
            }
        }
        if(!availableAudioSourceFound) 
        {
            var audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.clip = soundToPlay;
            audioSource.Play();
            soundAudioSources.Add(audioSource);
        }*/
    }

    private void QueueSound(Sound sound) 
    {
        soundQueue.Enqueue(sound);
        if(soundCoroutine == null) soundCoroutine = StartCoroutine(PlaySoundsCoroutine());
    }

    private IEnumerator PlaySoundsCoroutine() 
    {
        while(soundQueue.Count > 0) 
        {
            currentSound = soundQueue.Dequeue();
            soundAudioSource.transform.position = currentSound.Origin.transform.position;
            soundAudioSource.clip = currentSound.AudioClip;
            soundAudioSource.Play();
            yield return new WaitUntil(() => !soundAudioSource.isPlaying);
        }
        soundCoroutine = null;
    }
}