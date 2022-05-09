using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Flower : MonoBehaviour
{
    public static event Action<bool, Flower> OnProximity;
    public enum FlowerState
    {
        Grounded,
        Pickup
    }

    public FlowerState flowerState;
    public Vector3 rotateDirection;
    public float magnitude;
    public float frequency;

    public bool playerEntered;
    public bool playerExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerEntered = true;
            print("enter");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerExit = true;
            print("bruhf");
        }
    }

    private void Update()
    {
        switch (flowerState)
        {
            case FlowerState.Grounded :
                HoverVisual();
                CheckPlayerClose();
                break;
            
            case FlowerState.Pickup :
                
                break;
        }
    }

    private void CheckPlayerClose()
    {
        if (playerEntered)
        {
            OnProximity?.Invoke(true, this);
            playerEntered = false;
        }

        if (playerExit)
        { 
            OnProximity?.Invoke(false, this);
            playerEntered = false;
        }
    }


    private void HoverVisual()
    {
        //transform.Rotate( rotateDirection.normalized * Time.deltaTime * speed);
        //transform.Rotate( (Mathf.Sin(Time.time * frequency)) * magnitude * (rotateDirection.normalized));
        Quaternion sinRotation = Quaternion.Euler(magnitude * Mathf.Sin(Time.time * frequency) * rotateDirection);
        Quaternion yRotate = Quaternion.Euler(transform.rotation.x,transform.rotation.y + Time.time * 10f, transform.rotation.z);

        transform.rotation = sinRotation * yRotate;
    }
    
    
}
    
   