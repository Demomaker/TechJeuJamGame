using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
    private bool alreadyPressed = false;
    [SerializeField] private KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private string mainSceneName;
    [SerializeField] private string menuSceneName;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        StartCoroutine(GoToMainMenuScene());
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        Finder.OnPlayEventChannel.Notify += GoToMainScene;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        Finder.OnPlayEventChannel.Notify -= GoToMainScene;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if(Input.GetKeyUp(menuKey) && !alreadyPressed) 
        {
            alreadyPressed = true;
            StartCoroutine(GoToMainMenuScene());
        }
    }

    public void GoToMainScene() 
    {
        StartCoroutine(GoToMainSceneCoroutine());
    }

    public IEnumerator GoToMainSceneCoroutine() 
    {
        SceneManager.LoadScene(mainSceneName, LoadSceneMode.Additive);
        yield return null;
        if(SceneManager.GetSceneByName(menuSceneName).isLoaded)
            SceneManager.UnloadScene(menuSceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(mainSceneName));
        Finder.GameController.NewMatch();
    }

    public IEnumerator GoToMainMenuScene() 
    {
        SceneManager.LoadScene(menuSceneName, LoadSceneMode.Additive);
        yield return null;
        if(SceneManager.GetSceneByName(mainSceneName).isLoaded)
            SceneManager.UnloadScene(mainSceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(menuSceneName));
        alreadyPressed = false;
    }

}