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
        Say(LetterPart);
    }

    public void OnKiss() 
    {
        if(personIsTheChosenOne)
        {
            Say("You Won My Heart <3");
            Finder.OnWinEventChannel.Publish();
        }
        else 
        {
            Slap();
        }
    }

    private void Slap()
    {
        Say("Not COOL MAN! ***SLAP***");
    }

    private void Say(string messageText) 
    {
        Finder.ChatController.QueueMessage(new Message(this.gameObject.name, messageText, 20f));
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