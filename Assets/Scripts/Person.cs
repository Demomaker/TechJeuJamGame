using UnityEngine;

public class Person : MonoBehaviour
{
    private bool personIsTheChosenOne => this == Finder.GameController.CurrentPersonToFind;
    private int detailToKeepOut = 0;
    [SerializeField] private Vector3 startingPosition;
    public Language Language;
    public HairColor HairColor;
    public Interest Interest;
    public Relation Relation;
    public string LetterPart = "";
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        detailToKeepOut = Random.Range(0,3);
    }
    public void ResetPosition() 
    {
        transform.position = startingPosition;
        detailToKeepOut = Random.Range(0,3);
    }
    public void OnFirstChat() 
    {
        RevealMessagePart();
    }

    public void OnSecondChat() 
    {
        SayDetails();
    }

    public void ExpressJealousy() 
    {
        if(Relation == Relation.Mate)
            Say("WHY!?!? I THOUGHT WE WERE SUCH GOOD MATES! I GUESS NOT NOW THAT YOU BETRAYED ME :(");
    }

    private void SayDetails() 
    {
        string chatMessageStart = "All I can tell you is that...";
        string languageMessage = " I'm " + Language + ".";
        string hairColorMessage = " My hair is " + HairColor + ".";
        string interestMessage = " I like " + Interest + ".";
        string relationMessage = " The link between you and me is that I'm your " + Relation + ".";
        string chatEndMessage = " Thank you for chatting with me, hope to see you soon!";
        switch(detailToKeepOut) 
        {
            case 0 :
                languageMessage = "";
            break;
            case 1 :
                hairColorMessage = "";
            break;
            case 2 :
                interestMessage = "";
            break;
            case 3 :
                relationMessage = "";
            break;
        }
        Say(chatMessageStart + languageMessage + hairColorMessage + interestMessage + relationMessage + chatEndMessage);
    }

    private void RevealMessagePart()
    {
        if(LetterPart == "") Say("Sorry, I don't know what this says honestly, better luck with someone else.");
        else Say("I can only read some parts of this letter, but from what I can tell, this person said : " + LetterPart);
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
            Finder.OnLossEventChannel.Publish();
        }
    }

    private void Slap()
    {
        Say("Not Cool, Man!");
        Finder.OnSoundPlayEventChannel.Publish(this.gameObject, Finder.GameController.SlapSoundEffect);
    }

    private void Say(string messageText) 
    {
        Finder.ChatController.QueueMessage(new Message(this.gameObject.name, messageText, 5f));
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