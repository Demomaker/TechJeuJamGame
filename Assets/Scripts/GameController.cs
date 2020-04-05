using System;
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
    private List<string> names = new List<string>()
    {
        "Myrna",
        "Gertude",
        "Benita",
        "Criselda",
        "Pamila",
        "Pamula" ,
        "Beverlee",
        "Wendie" ,
        "Arianna" ,
        "Kimiko" ,
        "Mandy" ,
        "Tatiana" ,
        "Tamika" ,
        "Genoveva" ,
        "Rolande" ,
        "Danette" ,
        "Marni" ,
        "Violet" ,
        "Renita" ,
        "Aurora" ,
        "Marth" ,
        "Akiko" ,
        "Jane" ,
        "Latia" ,
        "Joanna" ,
        "Inga" ,
        "Thomasina",
        "Flo" ,
        "Isabella" ,
        "Britney"
    };

    private List<GameObject> peopleGameObjects = new List<GameObject>();
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject initialLetter;
    [SerializeField] private AudioClip slapSoundEffect;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] peoplePrefabs;

    public Person CurrentPersonToFind { get { return currentPersonToFind; } }
    public Person[] People => GetPeoplePersons();
    public bool GameEnded => gameEnded;
    public AudioClip SlapSoundEffect => slapSoundEffect;

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

    private void OnWin()
    {
        StartCoroutine(FinalizeWinAndStartNewMatch());
    }

    private void OnLoss()
    {
        StartCoroutine(FinalizeLossAndStartNewMatch());
    }

    private Person[] GetPeoplePersons() 
    {
        List<Person> persons = new List<Person>();
        Person person = null;
        for(int i = 0; i < peopleGameObjects.Count; i++)
        {
            person = peopleGameObjects[i].GetComponent<Person>();
            if(person != null) persons.Add(person);
        }
        return persons.ToArray();
    }

    private IEnumerator FinalizeWinAndStartNewMatch()
    {
        gameEnded = true;
        foreach (Person person in People) person.ExpressJealousy();
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
        Say(correctGirlName, "YOU MONSTER! HOW COULD YOU NOT NOTICE IT WAS ME ALL ALONG?!?!?");
        yield return new WaitForSeconds(16f);
        NewMatch();
        gameEnded = false;
    }

    public void NewMatch()
    {
        for(int i = peopleGameObjects.Count - 1; i >= 0; i--) 
        {
            Destroy(peopleGameObjects[i]);
        }
        peopleGameObjects.Clear();
        ChoosePeopleGameObjects();
        GeneratePeopleTraits();
        PlacePeople();
        ChoosePerson();
        WriteLetter();
        SeperateLetterIntoPeople();
        player.transform.position = startingPosition;
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ShowInitialLetterCoroutine());
    }

    private void PlacePeople()
    {
        List<Transform> tempSpawnPointList = new List<Transform>(spawnPoints);
        Transform chosenSpawnPoint = null;
        for(int i = 0; i < People.Length; i++) 
        {
            chosenSpawnPoint = tempSpawnPointList[UnityEngine.Random.Range(0, tempSpawnPointList.Count)];
            People[i].transform.position = chosenSpawnPoint.position;
            People[i].transform.rotation = chosenSpawnPoint.rotation;
            tempSpawnPointList.Remove(chosenSpawnPoint);
        }
    }

    private void GeneratePeopleTraits()
    {
        List<string> tempNames = new List<string>(names);
        string chosenName = "";
        foreach(Person person in People) 
        {
            person.HairColor = (HairColor) UnityEngine.Random.Range(0, Enum.GetNames(typeof(HairColor)).Length);
            person.Interest = (Interest) UnityEngine.Random.Range(0, Enum.GetNames(typeof(Interest)).Length);
            person.Relation = (Relation) UnityEngine.Random.Range(0, Enum.GetNames(typeof(Relation)).Length);
            person.Language = (Language) UnityEngine.Random.Range(0, Enum.GetNames(typeof(Language)).Length);
            chosenName = tempNames[UnityEngine.Random.Range(0,tempNames.Count - 1)];
            person.gameObject.name = chosenName;
            tempNames.Remove(chosenName);
        }
    }

    private void ChoosePeopleGameObjects()
    {
        List<GameObject> tempPeopleGameObjects = new List<GameObject>();
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            tempPeopleGameObjects.Add(GameObject.Instantiate(peoplePrefabs[UnityEngine.Random.Range(0, peoplePrefabs.Length)]));
        }
        peopleGameObjects = tempPeopleGameObjects;
    }

    private void ChoosePerson()
    {
        currentPersonToFind = People[UnityEngine.Random.Range(0, People.Length)];
    }

    private void WriteLetter()
    {
        letter = "Hello my love! I'm " + currentPersonToFind.Language + ". | My hair is " + currentPersonToFind.HairColor + ". | I like " + currentPersonToFind.Interest + ". | I'm your " + currentPersonToFind.Relation + ". Try to find out who I am by making connections. I love you <3 xoxoxo";
    }

    private void SeperateLetterIntoPeople()
    {
        List<Person> tempPeople = new List<Person>(People);
        int numberOfPeople = tempPeople.Count;
        string[] letterParts = letter.Split('|');
        for(int i = 0; i < tempPeople.Count; i++) 
        {
            tempPeople[i].LetterPart = "";
        }
        for (int i = 0; i < letterParts.Length; i++)
        {
            Person chosenPerson = tempPeople[UnityEngine.Random.Range(0, tempPeople.Count)];
            chosenPerson.LetterPart = letterParts[i];
            tempPeople.Remove(chosenPerson);
        }
    }

    private IEnumerator ShowInitialLetterCoroutine()
    {
        initialLetter.SetActive(true);
        yield return new WaitForSeconds(6f);
        initialLetter.SetActive(false);
    }
}