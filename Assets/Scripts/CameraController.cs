using UnityEngine;

public class CameraController : MonoBehaviour 
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraOffset;
    void FixedUpdate()
    {
        this.transform.position = player.transform.position + cameraOffset;
        this.transform.rotation = player.transform.rotation;
    }
}