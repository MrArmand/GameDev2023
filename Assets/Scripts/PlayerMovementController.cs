using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float ySpeed;

    private Rigidbody2D rb2D;
    private Vector2 velocity = new Vector2();

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        velocity = rb2D.velocity;

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            velocity.y = ySpeed;
        }

        rb2D.velocity = velocity;


    }
    
}

