using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    [SerializeField] private TMP_Text achievementText;
    [SerializeField] private GameObject achievementsUI;
    Queue<string> achievementQueue = new Queue<string>();
    private bool processQueue = false;

    private void OnEnable()
    {
        PlayerMovementController.OutOfFuel += PlayerMovementController_OutOfFuel;
        PlayerMovementController.OutOfBounds += PlayerMovementController_OutOfBounds;
        PlayerMovementController.Points1000 += PlayerMovementController_Points1000;
    }

    private void OnDisable()
    {
        PlayerMovementController.OutOfFuel -= PlayerMovementController_OutOfFuel;
        PlayerMovementController.OutOfBounds -= PlayerMovementController_OutOfBounds;
        PlayerMovementController.Points1000 -= PlayerMovementController_Points1000;
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

    private void PlayerMovementController_Points1000(string achievement)
    {
        achievementQueue.Enqueue(achievement);
        Debug.Log(achievement);
    }

    private IEnumerator ProcessAchievements()
    {

        while (achievementQueue.Count > 0)
        {
            achievementsUI.SetActive(true);
            processQueue = true;
            string achievement = achievementQueue.Dequeue();
            Debug.Log(achievement);
            achievementText.text = achievement;

            yield return new WaitForSecondsRealtime(5f); // Wait for 5 seconds before processing the next achievement
            processQueue = false;
            achievementsUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (achievementsUI.activeSelf)
        {

            achievementText.ForceMeshUpdate();
            var textInfo = achievementText.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible) continue;

                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; j++)
                {
                    var orig = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.unscaledTime * 2f + orig.x * 0.01f) * 10f, 0);
                }
            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                achievementText.UpdateGeometry(meshInfo.mesh, i);
            }
        }


        if (processQueue == false)
        {
            StartCoroutine(ProcessAchievements());
        }
    }
}
