using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectStay : MonoBehaviour
{
    private Rigidbody _rb;
    private List<Rigidbody> connectedRbs = new List<Rigidbody>();

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("beep");

            //other.GetComponent<PlayerMovementController>().controller.attachedRigidbody.velocity = new Vector3(300, 300, 300);
            // PlayerMovementController playerMovement = other.GetComponent<PlayerMovementController>();
            //  playerMovement._currentMovement += playerMovement._currentMovement + rb.velocity;
        }
        else if(other.CompareTag("Interactable"))
        {
            connectedRbs.Add(other.GetComponent<Rigidbody>());
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Interactable"))
        {
            connectedRbs.Remove(other.GetComponent<Rigidbody>());
            // rb.transform.position = new Vector3(other.transform.position.x, rb.transform.position.y,
            //    other.transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        if (connectedRbs.Count <= 0) return;
        
        foreach (Rigidbody connectedRb in connectedRbs)
        {
            connectedRb.velocity = _rb.velocity;
        }
    }

  
}
