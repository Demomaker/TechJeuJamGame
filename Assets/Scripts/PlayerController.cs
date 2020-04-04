using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    private float closestPersonDistance = 100f;
    private const float mouseRotationAdjustment = 10f;
    private float yaw;
    [SerializeField] private float maxDistanceFromPersonToPerformAction = 10f;
    [SerializeField] private float mouseSensitivity = 1.0f;

    [SerializeField] private KeyCode forwardKey = KeyCode.W;
    [SerializeField] private KeyCode backwardKey = KeyCode.S;
    [SerializeField] private KeyCode leftSideKey = KeyCode.A;
    [SerializeField] private KeyCode rightSideKey = KeyCode.D;
    void FixedUpdate()
    {
        Move();
        Rotate();
        if(CanPerformActions())
            PerformActions();
    }

    private void Move() 
    {
        if(Input.GetKey(forwardKey)) MoveInDirection(transform.forward);
        if(Input.GetKey(backwardKey)) MoveInDirection(-transform.forward);
        if(Input.GetKey(rightSideKey)) MoveInDirection(transform.right);
        if(Input.GetKey(leftSideKey)) MoveInDirection(-transform.right);
    }

    private void MoveInDirection(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime;
    }

    private void Rotate()
    {
        yaw += mouseSensitivity * mouseRotationAdjustment * Input.GetAxis("Mouse X");
       
        // Wrap yaw:
        while (yaw < 0f) {
            yaw += 360f;
        }
        while (yaw >= 360f) {
            yaw -= 360f;
        }
       
        // Set orientation:
        transform.eulerAngles = new Vector3(0f, yaw, 0f);
    }

    private void PerformActions() 
    {
        if(Finder.ClosestPerson != null) 
        {
            if(Input.GetKeyUp(KeyCode.Mouse0)) PerformLeftClick();
            if(Input.GetKeyUp(KeyCode.Mouse1)) PerformRightClick();
        }
        else 
        {
            Finder.ChatController.ClearQueue();
        }
    }

    private void PerformLeftClick() 
    {
        SayAndClear("Hey, I have this letter, but I can't read it, could you tell me what it says?");
        Finder.ClosestPerson.OnFirstChat();
        Say("I'm not very good at noticing details, can you tell me some about yourself?");
        Finder.ClosestPerson.OnSecondChat();
    }

    private void PerformRightClick() 
    {
        SayAndClear("Smooch!");
        Finder.ClosestPerson.OnKiss();
    }

    private void SayAndClear(string messageText) 
    {
        Finder.ChatController.ClearQueue();
        Say(messageText);
    }
    private void Say(string messageText) 
    {
        Finder.ChatController.QueueMessage(new Message(this.gameObject.name, messageText, 4f));
    }

    private bool CanPerformActions() 
    {
        return !Finder.GameController.GameEnded;
    }
}