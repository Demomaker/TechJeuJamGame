using UnityEngine;

public static class Finder 
{
    private static OnWinEventChannel onWinEventChannel = null;
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
}