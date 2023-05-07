using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 3f;
    [SerializeField] private float maxDistanceFromStart = 5f;
    [SerializeField] private float smoothTime = 0.3f;

    private Camera cam;
    private Vector3 startPosition;
    private Vector3 velocity;

    private void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = 2;
        offset = transform.position - player.position;
        startPosition = transform.position;
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
    }

   
    private void FixedUpdate()
    {
        float distanceFromStart = Vector3.Distance(startPosition, transform.position);
        if (distanceFromStart <= maxDistanceFromStart)
        {
            Vector3 desiredPosition = player.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        }
        else
        {
            
            transform.position = Vector3.Lerp(transform.position, startPosition, smoothTime);
        }
    }
}
