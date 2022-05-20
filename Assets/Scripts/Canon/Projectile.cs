using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Lean.Pool;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SphereCollider sphereCollider;
    public MeshRenderer meshRenderer;
    public GameObject fakeShadow;
    public bool alive = true;
    public GameObject ballModel;
    public GameObject matrixModel;
    public GameObject flowerModel;
    public bool flower;
    public ParticleSystem particle;
    public ParticleSystem playerKill;

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
        if (MatrixManager.worldState == MatrixManager.WorldState.Matrix)
        {
            ballModel.SetActive(false);
            matrixModel.SetActive(true);
        }
        else
        {
            if (flower)
            {
                flowerModel.SetActive(true);
                ballModel.SetActive(false);
            }
            else
            {
                ballModel.SetActive(true);
                flowerModel.SetActive(false);
            }
          
            matrixModel.SetActive(false);
        }
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
            LeanPool.Spawn(playerKill);
        }
        
        
    }

    public void FakeDestroy()
    {
        if (MatrixManager.worldState == MatrixManager.WorldState.Matrix) return;
        
        alive = false;
        Destroy();
        
        if (flower) return;

        LeanPool.Spawn(particle, transform.position, Quaternion.identity);
        particle.transform.Rotate(0,180,0);
        //particle.Play();
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
