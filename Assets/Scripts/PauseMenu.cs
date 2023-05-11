using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenuUI;
    public static bool gameIsPaused = false;
    private PlayerMovementController player;

    public Animator transition;
    public float transitionTime = 1f;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Rewind()
    {
        player.Rewind();
        Debug.Log("REWINDING THE GAME.");
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }

}
