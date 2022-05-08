using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public MeshRenderer meshRenderer;
    public bool alive = true;

    public static event Action OnCollisionWithPlayer;
    private MatrixManager matrixManager;
    private void Awake()
    {
        matrixManager = FindObjectOfType<MatrixManager>();
        
    }

    private void Update()
    {
        //transform.position = transform.position + transform.forward * Time.unscaledDeltaTime * 3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (MatrixManager.worldState == MatrixManager.WorldState.TransitioningToReal) return;
        
        FakeDestroy();
        
        if (other.CompareTag("Player"))
        {
            if(MatrixManager.worldState == MatrixManager.WorldState.Matrix) return;
            OnCollisionWithPlayer?.Invoke();
            Alive();
        }
        
        
    }

    public void FakeDestroy()
    {
        alive = false;
        Destroy();
    }

    public void Destroy()
    {
        sphereCollider.enabled = false;
        meshRenderer.enabled = false;
    }

    public void Alive()
    {
        sphereCollider.enabled = true;
        meshRenderer.enabled = true;
    }

    public void SetAlive()
    {
        alive = true;
        Alive();
    }
    
}
