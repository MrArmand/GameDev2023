using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Profiles : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI output;

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
        Debug.Log("Score: " + score);
        output.text = score.ToString();
    }
}
