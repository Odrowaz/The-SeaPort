using System;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger with {other.gameObject.name}");
    }
}
