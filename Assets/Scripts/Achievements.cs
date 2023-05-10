using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    [SerializeField] private TMP_Text achievementText;
    [SerializeField] private GameObject achievementsUI;
    Queue<string> achievementQueue = new Queue<string>();
    private bool outOfFuel = false;

    private void OnEnable()
    {
        PlayerMovementController.OutOfFuel += PlayerMovementController_OutOfFuel;
        PlayerMovementController.OutOfBounds += PlayerMovementController_OutOfBounds;
    }

    private void OnDisable()
    {
        PlayerMovementController.OutOfFuel -= PlayerMovementController_OutOfFuel;
        PlayerMovementController.OutOfBounds -= PlayerMovementController_OutOfBounds;
    }

    private void PlayerMovementController_OutOfFuel(string achievement)
    {
        achievementQueue.Enqueue(achievement);
        Debug.Log(achievement); 
    }

    private void PlayerMovementController_OutOfBounds(string achievement)
    {
        achievementQueue.Enqueue(achievement);
        Debug.Log(achievement);
    }

    private IEnumerator ProcessAchievements()
    {

        while (achievementQueue.Count > 0)
        {
            achievementsUI.SetActive(true);
            outOfFuel = true;
            string achievement = achievementQueue.Dequeue();
            Debug.Log(achievement);
            achievementText.text = achievement;
            // Add your achievement processing logic here

            yield return new WaitForSecondsRealtime(5f); // Wait for 5 seconds before processing the next achievement
            outOfFuel = false;
            achievementsUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (outOfFuel == false)
        {
            StartCoroutine(ProcessAchievements());
        }
    }
}
