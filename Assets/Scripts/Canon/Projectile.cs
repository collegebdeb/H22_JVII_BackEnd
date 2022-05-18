using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public MeshRenderer meshRenderer;
    public GameObject fakeShadow;
    public bool alive = true;
    public GameObject ballModel;
    public GameObject flowerModel;
    public bool flower;

    public void SwitchToFlowerModel()
    {
        flower = true;
        ballModel.SetActive(false);
        flowerModel.SetActive(true);
    }

    public static event Action OnCollisionWithPlayer;
    private MatrixManager matrixManager;
    
    public ParticleSystem particleFire;
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
        if (flower)
        {
            FakeDestroy();
            return;
        }
        if (other.CompareTag("Flower"))
        {
            return;
        }
        
        if (MatrixManager.worldState == MatrixManager.WorldState.TransitioningToReal) return;
       
        if (other.CompareTag("ProjectileBypass")) return;
        FakeDestroy();
        
        if (other.CompareTag("Player"))
        {
            //if (other.GetComponent<BoxCollider>().isTrigger) return;
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
        particleFire.gameObject.SetActive(false);
        fakeShadow.SetActive(false);
    }

    public void Alive()
    {
        sphereCollider.enabled = true;
        meshRenderer.enabled = true;
        particleFire.gameObject.SetActive(true);
        fakeShadow.SetActive(true);
    }

    public void SetAlive()
    {
        alive = true;
        Alive();
    }
    
}
