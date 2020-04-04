using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    private Person currentPersonToFind = null;
    private string letter = "";
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private Person[] people;
    [SerializeField] private GameObject player;

    public Person CurrentPersonToFind { get{ return currentPersonToFind;}}
    public Person[] People { get {return people;}}

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable() 
    {
        Finder.OnWinEventChannel.Notify += OnWin;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable() 
    {
        Finder.OnWinEventChannel.Notify -= OnWin;
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
        NewMatch();
    }

    private void NewMatch() 
    {
        ResetPlayerPosition();
        ChoosePerson();
        WriteLetter();
        SeperateLetterIntoPeople();
    }
    private void ResetPlayerPosition() 
    {
        player.transform.position = startingPosition;
    }
    private void ChoosePerson() 
    {
        currentPersonToFind = people[Random.Range(0, people.Length)];
    }

    private void WriteLetter() 
    {
        letter += currentPersonToFind.Language + " | " + currentPersonToFind.HairColor + " | " + currentPersonToFind.Interest + " | " + currentPersonToFind.Relation + " <3 xoxoxo";
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
}