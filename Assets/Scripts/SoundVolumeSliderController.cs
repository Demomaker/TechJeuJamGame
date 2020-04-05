using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeSliderController : MonoBehaviour 
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        GetComponent<Slider>().value = Finder.AudioController.SoundVolume;
    }
    public void ChangeVolume(float volume) 
    {
        Finder.OnSoundVolumeChanged.Publish(volume);
    }
}