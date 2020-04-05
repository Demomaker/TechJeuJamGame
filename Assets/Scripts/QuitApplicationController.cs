using UnityEditor;
using UnityEngine;

public class QuitApplicationController : MonoBehaviour 
{
    public void QuitApplication() 
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}