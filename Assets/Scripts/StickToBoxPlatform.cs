using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToBoxPlatform : MonoBehaviour
{
   private Rigidbody _rb;
   [SerializeField] private Rigidbody platformRb;
   public bool connected;

   private InteractableBox interactableBox;
   
   
   
   private void Awake()
   {
      _rb = GetComponentInParent<Rigidbody>();
      interactableBox = GetComponentInParent<InteractableBox>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (interactableBox.disAllowBoxSnap)
      {
         interactableBox.DisconnectSelfToPlatformBox(other.GetComponent<Rigidbody>());
         return;
      }
      
      
      if (other.CompareTag("Interactable"))
      {
         interactableBox.ConnectSelfToPlatformBox(other.GetComponent<Rigidbody>());
      }
      else if(other.CompareTag("Platform"))
      {
         //interactableBox.SetIsGrounded(true);
      }
   }

   private void OnTriggerStay(Collider other)
   {
      if (interactableBox.disAllowBoxSnap)
      {
         interactableBox.DisconnectSelfToPlatformBox(other.GetComponent<Rigidbody>());
         return;
      }
      if (interactableBox.state == InteractableBox.BoxState.Drag) return;
      if (other.CompareTag("Interactable"))
      {
         interactableBox.ConnectSelfToPlatformBox(other.GetComponent<Rigidbody>());
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Interactable"))
      {
         interactableBox.DisconnectSelfToPlatformBox(other.GetComponent<Rigidbody>());
      }
      else if(other.CompareTag("Platform"))
      {
        // interactableBox.SetIsGrounded(false);
      }
   }
   
  
   
   private void FixedUpdate()
   {
      /**
      if (platformRbs.Count <= 0)
      {
         connected = false;
         return;
      }
      
      connected = true;

      foreach (Rigidbody platformRb in platformRbs)
      {
         _rb.velocity = platformRb.velocity;
      }
      **/
   }
}
