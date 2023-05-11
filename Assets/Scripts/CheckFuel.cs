using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckFuel : MonoBehaviour
{
    public UnityEvent<GameObject> OnTriggerChange;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Fuel")
        {
            OnTriggerChange?.Invoke(other.gameObject);
            Destroy(other);
        }
    }
}
