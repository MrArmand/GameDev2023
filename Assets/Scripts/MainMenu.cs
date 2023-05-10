using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject optionsMenuUI;
    [SerializeField] private TMP_Text levelText;
    public Animator transition;
    public float transitionTime = 1f;
    private int levelID = 1;
    private string validKeys = "qwertyuiopasdfghjklzxcvbnm".ToUpper();

    public void Start()
    {
        SaveGame.LoadProgress();
        if (SaveGame.LevelID != 0)
        {
            levelID = SaveGame.LevelID;
        }
    
        Debug.Log("LEVEL ID: " + levelID);
    }

    public void PlayGame()
    {
        StartCoroutine(LoadLevel());
    }

    public void Settings()
    {
        mainMenuUI.SetActive(false);
        levelText.text = "LEVEL " + levelID;
        optionsMenuUI.SetActive(true);
    }

    public void Back()
    {
        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void FlyKey(string key)
    {
        key = key.ToUpper();

        Debug.Log(key);

        if (validKeys.Contains(key) & key != "")
        {
            KeyCode newKey = (KeyCode)Enum.Parse(typeof(KeyCode), key);
            KeySettings.Up = newKey;
            Debug.Log(newKey);
        }
    }

    public void LeftKey(string key)
    {
        key = key.ToUpper();

        Debug.Log(key);

        if (validKeys.Contains(key) & key != "")
        {
            KeyCode newKey = (KeyCode)Enum.Parse(typeof(KeyCode), key);
            KeySettings.Left = newKey;
            Debug.Log(newKey);
        }
    }

    public void RightKey(string key)
    {
        key = key.ToUpper();

        Debug.Log(key);

        if (validKeys.Contains(key) & key != "")
        {
            KeyCode newKey = (KeyCode)Enum.Parse(typeof(KeyCode), key);
            KeySettings.Right = newKey;
            Debug.Log(newKey);
        }
    }

    public void Level()
    {
        if (levelID == 1)
        {
            levelID++;
        }
        else if (levelID == 2)
        {
            levelID--;
        }

        SaveGame.LevelID = levelID;
        SaveGame.SaveProgress();
        levelText.text = "LEVEL " + levelID;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelID);
    }
}

