using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Profiles : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI output;
    public TextMeshProUGUI achievements;

    public void Start()
    {
        int chosenProfile = PlayerPrefs.GetInt("ChosenProfile");
        dropdown.value = chosenProfile;
        HandleInputData(chosenProfile);
    }

    public void HandleInputData(int chosenProfile)
    {
        PlayerPrefs.SetInt("ChosenProfile", chosenProfile);
        Debug.Log(chosenProfile);
        SaveGame.ProfileID = chosenProfile;
        SaveGame.LoadProgress();
        int score = SaveGame.Score;
        int achievementsCount = 0;
        if (SaveGame.Outofbounds)
        {
            achievementsCount++;
        }

        if(SaveGame.Outoffuel)
        {
            achievementsCount++;
        }

        if (SaveGame.Points1000)
        {
            achievementsCount++;
        }

        Debug.Log("Score: " + score);
        achievements.text = "Achievements: " + achievementsCount.ToString() + "/3";
        output.text = score.ToString();
    }
}
