using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectStay : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("beep");
           // PlayerMovementController playerMovement = other.GetComponent<PlayerMovementController>();
          //  playerMovement._currentMovement += playerMovement._currentMovement + rb.velocity;
        }
        else if(other.GetComponent<Interactable>())
        {
            //print(other.gameObject.name);
           // rb.transform.position = new Vector3(other.transform.position.x, rb.transform.position.y,
            //    other.transform.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
