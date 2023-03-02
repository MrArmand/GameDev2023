using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float thrust;
    [SerializeField] private float groundCheckDistance;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        
    }

    private void FixedUpdate()
    {
        // Apply thrust
        if (Input.GetKey(KeyCode.W))
        {
            Vector2 thrustVector = transform.up;
            rb.AddForce(thrust * thrustVector * Time.fixedDeltaTime);
        }
    }

    public void onGroundChange(GameObject _onGround)
    {
        Debug.Log("Hit the road Jack");
    }
}

