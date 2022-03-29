using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SphereCollider collider;
    public MeshRenderer meshRenderer;

    public static event Action OnCollisionWithPlayer;
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) OnCollisionWithPlayer?.Invoke();
        
        FakeDestroy();
    }

    public void FakeDestroy()
    {
        collider.enabled = false;
        meshRenderer.enabled = false;
    }

    public void ResetLoop()
    {
        collider.enabled = true;
        meshRenderer.enabled = true;
    }
}
