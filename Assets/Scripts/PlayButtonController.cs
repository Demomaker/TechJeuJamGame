using UnityEngine;

public class PlayButtonController : MonoBehaviour 
{
    public void Play() 
    {
        Finder.OnPlayEventChannel.Publish();
    }
}