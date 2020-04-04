using UnityEngine;

public class Person : MonoBehaviour
{
    private bool personIsTheChosenOne => this == Finder.GameController.CurrentPersonToFind;
    public Language Language;
    public HairColor HairColor;
    public Interest Interest;
    public Relation Relation;
    public string LetterPart = "";
    public void OnChat() 
    {
        RevealMessagePart();
    }

    private void RevealMessagePart()
    {
        Debug.Log(LetterPart);
    }

    public void OnKiss() 
    {
        if(personIsTheChosenOne)
        {
            Finder.OnWinEventChannel.Publish();
        }
        else 
        {
            Slap();
        }
    }

    private void Slap()
    {
        Debug.Log("NOT COOL MAN! ***SLAP***");
    }
}

public enum Language 
{
    French,
    English,
    Spanish,
    German,
    Japanese
}

public enum HairColor 
{
    Brown,
    Red,
    Blond,
    Black
}

public enum Interest
{
    Reading,
    Cooking,
    Hiking,
    Computers
}

public enum Relation 
{
    Cousin,
    Parent,
    Child,
    Mate
}