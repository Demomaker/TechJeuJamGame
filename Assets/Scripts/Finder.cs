using UnityEngine;

public static class Finder 
{
    private static OnWinEventChannel onWinEventChannel = null;
    private static OnLossEventChannel onLossEventChannel = null;
    private static OnPlayEventChannel onPlayEventChannel = null;
    private static OnMusicVolumeChanged onMusicVolumeChanged = null;
    private static OnSoundVolumeChanged onSoundVolumeChanged = null;
    private static OnSoundPlayEventChannel onSoundPlayEventChannel = null;
    private static AudioController audioController = null;
    private static GameController gameController = null;
    private static ChatController chatController = null;
    public static OnWinEventChannel OnWinEventChannel 
    {
        get
        {
            if(onWinEventChannel == null)
                onWinEventChannel = GameObject.FindObjectOfType<OnWinEventChannel>();
            return onWinEventChannel;
        }
    }
    public static OnLossEventChannel OnLossEventChannel 
    {
        get
        {
            if(onLossEventChannel == null)
                onLossEventChannel = GameObject.FindObjectOfType<OnLossEventChannel>();
            return onLossEventChannel;
        }
    }

    public static OnPlayEventChannel OnPlayEventChannel 
    {
        get 
        {
            if(onPlayEventChannel == null)
                onPlayEventChannel = GameObject.FindObjectOfType<OnPlayEventChannel>();
            return onPlayEventChannel;
        }
    }

    public static OnMusicVolumeChanged OnMusicVolumeChanged 
    {
        get 
        {
            if(onMusicVolumeChanged == null)
                onMusicVolumeChanged = GameObject.FindObjectOfType<OnMusicVolumeChanged>();
            return onMusicVolumeChanged;
        }
    }

    public static OnSoundVolumeChanged OnSoundVolumeChanged 
    {
        get 
        {
            if(onSoundVolumeChanged == null)
                onSoundVolumeChanged = GameObject.FindObjectOfType<OnSoundVolumeChanged>();
            return onSoundVolumeChanged;
        }
    }

    public static OnSoundPlayEventChannel OnSoundPlayEventChannel 
    {
        get 
        {
            if(onSoundPlayEventChannel == null)
                onSoundPlayEventChannel = GameObject.FindObjectOfType<OnSoundPlayEventChannel>();
            return onSoundPlayEventChannel;
        }
    }

    public static AudioController AudioController 
    {
        get 
        {
            if(audioController == null)
            {
                audioController = GameObject.FindObjectOfType<AudioController>();
            }
            return audioController;
        }
    }

    public static GameController GameController 
    {
        get
        {
            if(gameController == null) 
            {
                gameController = GameObject.FindObjectOfType<GameController>();
            }
            return gameController;
        }
    }

    public static ChatController ChatController 
    {
        get 
        {
            if(chatController == null)
            {
                chatController = GameObject.FindObjectOfType<ChatController>();
            }
            return chatController;
        }
    }

    public static Person ClosestPerson = null;
}