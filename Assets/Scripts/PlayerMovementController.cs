using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float xSpeed;
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
            velocity.x = xSpeed;
        }

        if (Input.GetKey(KeyCode.A)) 
        {
            velocity.x = -1f* xSpeed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            velocity.y = ySpeed;
        }

        rb2D.velocity = velocity;


    }
    
}

