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
    }

    public void HandleInputData(int chosenProfile)
    {
        Debug.Log("Chosen profile: " + chosenProfile);
        PlayerPrefs.SetInt("ChosenProfile", chosenProfile);
        SaveGame.ProfileID = chosenProfile;
        Debug.Log("Current profile: " + PlayerPrefs.GetInt("ChosenProfile"));
        int score = SaveGame.Score;
        output.text = score.ToString();
    }
}
