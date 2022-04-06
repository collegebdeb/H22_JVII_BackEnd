using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneCollider : MonoBehaviour
{
    public static event Action OnPlayerEnteredSafeZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("EnteredSafeZone - Auto removed player linked to box");
            OnPlayerEnteredSafeZone?.Invoke();
        }
    }
}