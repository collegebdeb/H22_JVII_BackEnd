using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectStay : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
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
            
            print("ss");
            other.GetComponent<Rigidbody>().velocity = _rb.velocity;

            // rb.transform.position = new Vector3(other.transform.position.x, rb.transform.position.y,
            //    other.transform.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Interactable"))
        {
            
            print("exit");
            other.GetComponent<Rigidbody>().velocity = other.GetComponent<Rigidbody>().velocity;

            // rb.transform.position = new Vector3(other.transform.position.x, rb.transform.position.y,
            //    other.transform.position.z);
        }
    }
}
