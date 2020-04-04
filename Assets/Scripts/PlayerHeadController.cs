using UnityEngine;

public class PlayerHeadController : MonoBehaviour
{
    private const float mouseRotationAdjustment = 10f;
    private float pitch;
    [SerializeField] private Transform player;
    [SerializeField] private float mouseSensitivity = 1.0f;
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        FindClosestPerson();
        RotateHead();
    }

    private void RotateHead() 
    {
        pitch += mouseSensitivity * mouseRotationAdjustment * Input.GetAxis("Mouse Y");
       
        // Clamp pitch:
        pitch = Mathf.Clamp(pitch, -180f, -25f);

        // Set orientation:
        transform.eulerAngles = new Vector3(0f, player.eulerAngles.y - 90, pitch);
    }

    private void FindClosestPerson() 
    {
        Vector3 initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 direction = Vector3.up;
        RaycastHit potentialPerson;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(initialPosition, transform.TransformDirection(direction), out potentialPerson))
        {
            Debug.DrawRay(initialPosition, transform.TransformDirection(direction) * potentialPerson.distance, Color.yellow);
            Person person = potentialPerson.transform.gameObject.GetComponent<Person>();
            if(person != null)
            {
                Finder.ClosestPerson = person;
            }
        }
        else 
        {
            Finder.ClosestPerson = null;
            Debug.DrawRay(initialPosition, transform.TransformDirection(direction) * 1000, Color.red);
        }
    }
}