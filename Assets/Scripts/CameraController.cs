using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;    // Reference to the player object
    public Vector3 offset;      // Offset between the camera and the player
    public float minZoomSize = 5.0f; // The minimum size the camera can zoom to
    public float maxZoomSize = 8.0f; // The maximum size the camera can zoom to
    public float zoomSpeed = 1.0f; // The speed at which the camera zooms in and out
    public float followSpeed = 5.0f; // The speed at which the camera follows the player

    private Camera _camera; // Reference to the Camera component
    private Vector3 _targetPosition; // The target position for the camera to move towards

    void Start()
    {
        _camera = GetComponent<Camera>();
        // Set the offset to match the camera's position in the editor
        offset = transform.position - player.position;
        // Set the initial target position to the player's position plus the offset
        _targetPosition = player.position + offset;
    }

    void Update()
    {
        // Zoom in and out using the mouse wheel
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (zoomDelta != 0.0f)
        {
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - zoomDelta, minZoomSize, maxZoomSize);
        }
    }

    void LateUpdate()
    {
        // Smoothly move the camera towards the target position
        Vector3 newPosition = Vector3.Lerp(transform.position, _targetPosition, followSpeed * Time.deltaTime);
        transform.position = newPosition;

        // Update the target position based on the player's position
        _targetPosition = player.position + offset;
    }
}
