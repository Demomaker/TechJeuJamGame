using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    private Person currentPersonToFind = null;
    private string letter = "";
    private bool gameEnded = false;
    private Coroutine currentCoroutine = null;
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private Person[] people;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject initialLetter;

    public Person CurrentPersonToFind { get{ return currentPersonToFind;}}
    public Person[] People { get {return people;}}
    public bool GameEnded => gameEnded;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable() 
    {
        Finder.OnWinEventChannel.Notify += OnWin;
        Finder.OnLossEventChannel.Notify += OnLoss;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable() 
    {
        Finder.OnWinEventChannel.Notify -= OnWin;
        Finder.OnLossEventChannel.Notify -= OnLoss;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        NewMatch();
    }

    private void OnWin() 
    {
        StartCoroutine(FinalizeWinAndStartNewMatch());
    }

    private void OnLoss() 
    {
        StartCoroutine(FinalizeLossAndStartNewMatch());
    }

    private IEnumerator FinalizeWinAndStartNewMatch() 
    {
        gameEnded = true;
        foreach(Person person in people) person.ExpressJealousy();
        yield return new WaitForSeconds(10f);
        NewMatch();
        gameEnded = false;
    }

    private void Say(string personName, string messageText) 
    {
        Finder.ChatController.QueueMessage(new Message(personName, messageText, 5f));
    }

    private IEnumerator FinalizeLossAndStartNewMatch() 
    {
        gameEnded = true; 
        string correctGirlName = GameObject.FindObjectsOfType<Person>().Where(obj => obj.GetComponent<Person>() == CurrentPersonToFind).First().gameObject.name;
        //CurrentPersonToFind.transform.position = new Vector3(player.transform.position.x - 4, player.transform.position.y + 10, player.transform.position.z - 4);
        Say(correctGirlName, "YOU MONSTER! HOW COULD YOU NOT NOTICE IT WAS ME ALL ALONG?!?!?");
        yield return new WaitForSeconds(16f);
        NewMatch();
        gameEnded = false;
    }

    private void NewMatch() 
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ShowInitialLetterCoroutine());
        ResetCharacterPositions();
        ChoosePerson();
        WriteLetter();
        SeperateLetterIntoPeople();
    }
    private void ResetCharacterPositions() 
    {
        player.transform.position = startingPosition;
        foreach(Person person in people) 
        {
            person.ResetPosition();
        }
    }
    private void ChoosePerson() 
    {
        currentPersonToFind = people[Random.Range(0, people.Length)];
    }

    private void WriteLetter() 
    {
        letter += "Hello my love! I'm " + currentPersonToFind.Language + ". | My hair is " + currentPersonToFind.HairColor + ". | I like " + currentPersonToFind.Interest + ". | I'm your " + currentPersonToFind.Relation + ". Try to find out who I am by making connections. I love you <3 xoxoxo";
    }

    private void SeperateLetterIntoPeople() 
    {
        List<Person> tempPeople = new List<Person>(people);
        int numberOfPeople = tempPeople.Count;
        string[] letterParts = letter.Split('|');
        for(int i = 0; i < numberOfPeople; i++)
        {
            Person chosenPerson = tempPeople[Random.Range(0, tempPeople.Count)];
            chosenPerson.LetterPart = letterParts[i];
            tempPeople.Remove(chosenPerson);
        }
    }

    private IEnumerator ShowInitialLetterCoroutine() 
    {
        initialLetter.SetActive(true);
        yield return new WaitForSeconds(3f);
        initialLetter.SetActive(false);
    }
}