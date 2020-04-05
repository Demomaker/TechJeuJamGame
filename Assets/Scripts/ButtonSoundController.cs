using UnityEngine;

public class ButtonSoundController : MonoBehaviour 
{
    public void PlayClickSound(AudioClip audioClip) 
    {
        Finder.OnSoundPlayEventChannel.Publish(this.gameObject, audioClip);
    }
}