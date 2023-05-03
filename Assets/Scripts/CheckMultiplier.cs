using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckMultiplier : MonoBehaviour
{
    [SerializeField] private int multiplier;
    public UnityEvent<int> OnTriggerChange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerChange?.Invoke(multiplier);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerChange?.Invoke(1);
    }
}
