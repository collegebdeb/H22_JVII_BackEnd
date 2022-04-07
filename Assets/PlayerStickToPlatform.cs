using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStickToPlatform : MonoBehaviour
{
    private Rigidbody _rb;

    private HandlePlayerMovement playerMovement;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
        playerMovement = GetComponentInParent<HandlePlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            playerMovement.ConnectToPlatform(other.GetComponent<Rigidbody>(), true);
        }
        else if(other.CompareTag("Platform"))
        {
            //interactableBox.SetIsGrounded(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            playerMovement.ConnectToPlatform(null, false);
        }
        else if(other.CompareTag("Platform"))
        {
            // interactableBox.SetIsGrounded(false);
        }
    }


}
