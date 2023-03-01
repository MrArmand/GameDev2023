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

        // Check for ground contact
        float spriteHeight = spriteRenderer.bounds.size.y;
        Vector2 checkPos = transform.position - Vector3.up * (spriteHeight / 2);
        RaycastHit2D hit = Physics2D.Raycast(checkPos, -Vector2.up, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;

        // Freeze game if player touches ground
        if (isGrounded)
        {
            Time.timeScale = 0f;
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
}

