using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideBALL : MonoBehaviour
{
    public static event Action OnCollideBall;
    private void OnTriggerEnter(Collider other)
    {
        if (!enabled) return;
        if (other.CompareTag("Projectile"))
        {
            if (!other.enabled) return;
            OnCollideBall?.Invoke();
        }
    }
}
