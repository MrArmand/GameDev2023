using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Profiles : MonoBehaviour
{
    public TextMeshProUGUI output;

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            output.text = "0";
        }


        if (val == 1)
        {
            output.text = "1000";
        }


        if (val == 2)
        {
            output.text = "Amogus";
        }
    }
}
