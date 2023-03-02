using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckGround : MonoBehaviour
{
    public UnityEvent<GameObject> OnTriggerChange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerChange?.Invoke(other.gameObject);
    }
}
