using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideBALL : MonoBehaviour
{
    public static event Action OnCollideBall;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            OnCollideBall?.Invoke();
        }
    }
}
