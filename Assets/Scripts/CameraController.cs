using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player; // Reference to the player object
    public Vector3 offset;   // Offset between the camera and the player

    void Start()
    {
        // Set the offset to match the camera's position in the editor
        GetComponent<Camera>().orthographicSize = 6; // Size u want to start with
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W)) // Also you can change E to anything
        {
            GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize - 1 * Time.deltaTime;
            if (GetComponent<Camera>().orthographicSize < 5)
            {
                GetComponent<Camera>().orthographicSize = 5; // Min size 
            }
        } else
        {
            GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize + 1 * Time.deltaTime;
            if (GetComponent<Camera>().orthographicSize > 8)
            {
                GetComponent<Camera>().orthographicSize = 8; // Max size
            }
        }
    }

    void LateUpdate()
    {
        // Set the camera's position to the player's position plus the offset
        transform.position = player.position + offset;
    }
}
