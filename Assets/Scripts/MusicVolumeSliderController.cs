using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSliderController : MonoBehaviour 
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        GetComponent<Slider>().value = Finder.AudioController.MusicVolume;
    }
    public void ChangeVolume(float volume) 
    {
        Finder.OnMusicVolumeChanged.Publish(volume);
    }
}