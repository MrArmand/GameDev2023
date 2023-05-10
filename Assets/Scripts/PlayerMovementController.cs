using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovementController : MonoBehaviour, IEntity
{
    [SerializeField] private Text fuelText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text velocityText;
    [SerializeField] private Text highestScoreText;
    [SerializeField] private int totalScore;
    [SerializeField] private int groundScore;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float thrust;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float fuel;
    [SerializeField] private float comsumption;
    [SerializeField] private float maxDistanceFromStart = 15f;
    [SerializeField] GameObject GameoverUI;

    private InputHandler inputHandler = new InputHandler();
    private LeftRotateCommand left;
    private RightRotateCommand right;
    private FlightCommand fly;

    public Transform collisionPoint;
    public SpriteRenderer thrusterSprite;
    public LayerMask groundLayer;
    public GameObject destroyLander;

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;

    private int multiplier = 1;
    private int currentHighScore;
    private float temporalVelocity;
    private float lastFuel;

    private Vector3 startPosition;
    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private bool canFly = true;
    private bool gameOver = false;
    
    float force = 100;

    public static event System.Action<string> OutOfFuel;
    private bool outoffuel = false;
    public static event System.Action<string> OutOfBounds;
    private bool outofbounds = false;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fuelText.text += fuel.ToString();
        scoreText.text += totalScore.ToString();
        velocityText.text += rb2D.velocity.ToString();
        // Get the current high score for the player
        SaveGame.LoadProgress();
        currentHighScore = SaveGame.Score;
        outoffuel = SaveGame.Outoffuel;
        outofbounds = SaveGame.Outofbounds;
        highestScoreText.text = currentHighScore.ToString();
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        startPosition = transform.position;
        lastFuel = fuel;
        canFly = true;
        gameOver = false;
        fly = new FlightCommand(this, thrust, rb2D);
        left = new LeftRotateCommand(this, rotationSpeed);
        right = new RightRotateCommand(this, rotationSpeed);
    }

    private void Update()
    {
        if (fuel <= 0)
        {
            gameOver = true;
            GameoverUI.SetActive(true);
            Time.timeScale = 0;
            addAchievement1();
        }

        inputHandler.LeftRotate(KeySettings.Left, left);
        inputHandler.RightRotate(KeySettings.Right, right);

        fuelText.text = "FUEL: " + ((int)fuel).ToString();
        if(canFly)
        {
            velocityText.text = "VELOCITY: " + rb2D.velocity.y.ToString("0.00");
        }
        
    }

    private void FixedUpdate()
    {
        float distanceFromStart = Vector3.Distance(startPosition, transform.position);
        
        if (distanceFromStart >= maxDistanceFromStart & canFly == true)
        {
            rb2D.position = new Vector2(0, 1);
            spriteRenderer.enabled = true;
            canFly = true;
            addAchievement2();
        }
        if (canFly)
        {
            inputHandler.Fly(KeySettings.Up, fly);
        }
        
        // Apply thrust
        if (inputHandler.IsFlying() & canFly == true)
        {
            thrusterSprite.enabled = true;
            fuel -= (comsumption * Time.fixedDeltaTime);   
        }
        else
        {
            thrusterSprite.enabled = false;
        }
    }
    public void Rewind()
    {
        if (!gameOver)
        {
            transform.position = lastPosition;
            transform.rotation = lastRotation;
            rb2D.velocity = Vector2.zero;
            spriteRenderer.enabled = true;
            canFly = true;
            fuel = lastFuel;
        }
    }

    public void MultiplierChange(int newMultiplier)
    {
        Debug.Log(newMultiplier);
        multiplier = newMultiplier;
    }

    public void onGroundChange(GameObject _onGround)
    {
        canFly = false;
        Debug.Log("Hit the road Jack");
       

        if (rb2D.velocity.y >= -0.5)
          {
                Time.timeScale = 0;
                // If the new score is higher than the current high score, update the high score
                totalScore += (groundScore * multiplier);

                if (totalScore > currentHighScore)
                {
                    SaveGame.Score = totalScore;
                    SaveGame.SaveProgress();
                    highestScoreText.text = totalScore.ToString();

                }
        }       
        else
        {
            Time.timeScale = 1;
            temporalVelocity = rb2D.velocity.y;
        
            velocityText.text = "VELOCITY: " + temporalVelocity.ToString("0.00");
            Instantiate(destroyLander, rb2D.transform.position, Quaternion.identity);
            Rigidbody2D[] rb = destroyLander.GetComponentsInChildren<Rigidbody2D>();
            foreach (Rigidbody2D particleRb in rb)
            {
                particleRb.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)); // Set a random rotation
                Vector2 direction = particleRb.velocity.normalized;
                particleRb.AddForce(direction * force); // Apply force based on velocity
            }

            spriteRenderer.enabled = false;
            rb2D.velocity = new Vector2(0, 0);
            Debug.Log("CRASH");
        }

        StartCoroutine(Freeze());


        // Freeze a game for 3 seconds 
        // If it has a fuel,it return the lander to start position
        // Otherwise it's the end of game

    }

    public void addAchievement1()
    {
        if (outoffuel == false)
        {
            OutOfFuel?.Invoke("RUN OUT OF FUEL");
            outoffuel = true;
            SaveGame.Outoffuel = outoffuel;
            SaveGame.SaveProgress();
        }
    }
    public void addAchievement2() 
    { 

        if (outofbounds == false & canFly == true)
        {
            OutOfBounds?.Invoke("TRY TO LEAVE MAP");
            outofbounds = true;
            SaveGame.Outofbounds = outofbounds;
            SaveGame.SaveProgress();
        }
    }
 
    IEnumerator Freeze()
    {
        
        scoreText.text = "SCORE: " + totalScore.ToString();
        yield return new WaitForSecondsRealtime(2f);

        if (fuel > 0 & gameOver == false)
        {
            Time.timeScale = 1;
            rb2D.position = new Vector2(0, 1); // starting position
            rb2D.velocity = new Vector2(0, 0);    // reset a speed
            rb2D.rotation = 0;                    // reset a rotation
            spriteRenderer.enabled = true;
            canFly = true;
            lastFuel = fuel;
        }
        else
        {
            Debug.Log("NO FUEL - END OF THE GAME");
        }
    }
}

