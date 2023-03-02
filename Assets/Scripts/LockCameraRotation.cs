using UnityEngine;

public class LockCameraRotation : MonoBehaviour
{
    private Vector3 initialRotation;

    private void Start()
    {
        // Store the initial rotation of the camera
        initialRotation = transform.eulerAngles;
    }

    private void LateUpdate()
    {
        // Reset the rotation of the camera to its initial value
        transform.eulerAngles = initialRotation;
    }
}
