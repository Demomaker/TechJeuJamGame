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
        closestPerson.OnChat();
    }

    private void PerformRightClick() 
    {
        closestPerson.OnKiss();
    }

    private void FindClosestPerson() 
    {
        Person[] people = Finder.GameController.People;
        Person closest = people[0];
        for(int i = 0; i < people.Length; i++)
        {
            if(Vector3.Distance(this.transform.position, closest.transform.position) > Vector3.Distance(this.transform.position, people[i].transform.position))
            {
                closest = people[i];
            }
        }
        closestPersonDistance = Vector3.Distance(this.transform.position, closest.transform.position);
        closestPerson = closest;
    }
}