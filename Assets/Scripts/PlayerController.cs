using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    private Person closestPerson = null;
    private float closestPersonDistance = 100f;
    private const float mouseRotationAdjustment = 10f;
    private float pitch;
    private float yaw;
    [SerializeField] private float maxDistanceFromPersonToPerformAction = 10f;
    [SerializeField] private float mouseSensitivity = 1.0f;

    [SerializeField] private KeyCode forwardKey = KeyCode.W;
    [SerializeField] private KeyCode backwardKey = KeyCode.S;
    [SerializeField] private KeyCode leftSideKey = KeyCode.A;
    [SerializeField] private KeyCode rightSideKey = KeyCode.D;
    void FixedUpdate()
    {
        FindClosestPerson();
        Move();
        Rotate();
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
        pitch += mouseSensitivity * mouseRotationAdjustment * -Input.GetAxis("Mouse Y");
        yaw += mouseSensitivity * mouseRotationAdjustment * Input.GetAxis("Mouse X");
       
        // Clamp pitch:
        pitch = Mathf.Clamp(pitch, -90f, 90f);
       
        // Wrap yaw:
        while (yaw < 0f) {
            yaw += 360f;
        }
        while (yaw >= 360f) {
            yaw -= 360f;
        }
       
        // Set orientation:
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    private void PerformActions() 
    {
        //if(maxDistanceFromPersonToPerformAction < closestPersonDistance) 
        //{
            if(Input.GetKeyUp(KeyCode.Mouse0)) PerformLeftClick();
            if(Input.GetKeyUp(KeyCode.Mouse1)) PerformRightClick();
        //}
    }

    private void PerformLeftClick() 
    {
        Say("Hey, I need some info...");
        closestPerson.OnChat();
    }

    private void PerformRightClick() 
    {
        Say("Smooch!");
        closestPerson.OnKiss();
    }

    private void FindClosestPerson() 
    {
        RaycastHit potentialPerson;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out potentialPerson))
        {
            Person person = potentialPerson.transform.gameObject.GetComponent<Person>();
            if(person != null)
            {
                closestPerson = person;
            }
        }
    }

    private void Say(string messageText) 
    {
        Finder.ChatController.ClearQueue();
        Finder.ChatController.QueueMessage(new Message(this.gameObject.name, messageText, 4f));
    }
}