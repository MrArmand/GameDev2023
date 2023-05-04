using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player; // Reference to the player object
    public Vector3 offset;   // Offset between the camera and the player

    void Start()
    {
        // Set the offset to match the camera's position in the editor
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Set the camera's position to the player's position plus the offset
        transform.position = player.position + offset;
    }
}
