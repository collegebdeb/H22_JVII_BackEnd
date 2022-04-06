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

   public static event Action<Rigidbody> OnBoxConnected;
   public static event Action<Rigidbody> OnBoxDisconnect;
   
   private void Awake()
   {
      _rb = GetComponentInParent<Rigidbody>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Interactable"))
      {
         OnBoxConnected?.Invoke(other.GetComponent<Rigidbody>());
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Interactable"))
      {
         OnBoxDisconnect?.Invoke(other.GetComponent<Rigidbody>());
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
