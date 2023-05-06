using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject optionsMenuUI;
    public Animator transition;
    public float transitionTime = 1f;

    public void PlayGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Settings()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void Back()
    {
        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}

