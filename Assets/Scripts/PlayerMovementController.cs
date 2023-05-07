using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Text fuelText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highestScoreText;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float thrust;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float fuel;
    [SerializeField] private float comsumption;
    [SerializeField] private int totalScore;
    [SerializeField] private int groundScore;
    public SpriteRenderer thrusterSprite;
    public LayerMask groundLayer;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private int multiplier = 1;
    private int currentHighScore;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fuelText.text += fuel.ToString();
        scoreText.text += totalScore.ToString();
        // Get the current high score for the player
        SaveGame.LoadProgress();
        currentHighScore = SaveGame.Score;
        highestScoreText.text = currentHighScore.ToString();
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

        fuelText.text = "FUEL: " + ((int)fuel).ToString();
    }

    private void FixedUpdate()
    {
        // Apply thrust
        if (Input.GetKey(KeyCode.W))
        {
            thrusterSprite.enabled = true;
            Vector2 thrustVector = transform.up;
            rb2D.AddForce(thrust * thrustVector * Time.fixedDeltaTime);
            fuel -= (comsumption * Time.fixedDeltaTime);   
        }
        else
        {
            thrusterSprite.enabled = false;
        }
    }


    public void MultiplierChange(int newMultiplier)
    {
        Debug.Log(newMultiplier);
        multiplier = newMultiplier;
    }

    public void onGroundChange(GameObject _onGround)
    {
        // If the new score is higher than the current high score, update the high score
        
        totalScore += (groundScore * multiplier);

        if (totalScore > currentHighScore)
        {
            SaveGame.Score = totalScore;
            SaveGame.SaveProgress();
            highestScoreText.text = totalScore.ToString();
        }

        

        Debug.Log("Hit the road Jack");

        StartCoroutine(Freeze());
        Time.timeScale = 0;
        // Freeze a game for 3 seconds 
        // If it has a fuel,it return the lander to start position
        // Otherwise it's the end of game
        
    }
    IEnumerator Freeze()
    {
        scoreText.text = "SCORE: " + totalScore.ToString();
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1;
        if (fuel > 0)
        {
            rb2D.position = new Vector2(0, 1); // starting position
            rb2D.velocity = new Vector2(0, 0);    // reset a speed
            rb2D.rotation = 0;                    // reset a rotation
        }
        else
        {
            Debug.Log("NO FUEL - END OF THE GAME");
        }
    }
}

