using UnityEngine;

public class CameraController : MonoBehaviour 
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerHead;
    [SerializeField] private Vector3 cameraOffset;
    void FixedUpdate()
    {
        this.transform.position = playerHead.transform.position + cameraOffset;
        this.transform.eulerAngles = new Vector3(-playerHead.transform.eulerAngles.z - 90, player.transform.eulerAngles.y, 0);
    }
}