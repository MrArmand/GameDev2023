using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public Transform player;
    public float movementScale = 0.1f;
    private Vector3 previousPlayerPosition;

    private void Start()
    {
        previousPlayerPosition = player.position;
    }

    private void Update()
    {
        Vector3 playerMovement = player.position - previousPlayerPosition;
        transform.position -= playerMovement * movementScale;
        previousPlayerPosition = player.position;
    }
}
