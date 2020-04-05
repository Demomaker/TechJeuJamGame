using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
    private bool alreadyPressed = false;
    [SerializeField] private KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private Object mainScene;
    [SerializeField] private Object menuScene;
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
        SceneManager.LoadScene(mainScene.name, LoadSceneMode.Additive);
        yield return null;
        SceneManager.UnloadScene(menuScene.name);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(mainScene.name));
        Finder.GameController.NewMatch();
    }

    public IEnumerator GoToMainMenuScene() 
    {
        SceneManager.LoadScene(menuScene.name, LoadSceneMode.Additive);
        yield return null;
        SceneManager.UnloadScene(mainScene.name);
        alreadyPressed = false;
    }

}