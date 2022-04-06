using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectStay : MonoBehaviour
{
   private Rigidbody _rb;
   [SerializeField] private List<Rigidbody> platformRbs = new List<Rigidbody>();
   public bool connected;
   
   private void Awake()
   {
      _rb = GetComponentInParent<Rigidbody>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Interactable"))
      {
         platformRbs.Add(other.GetComponent<Rigidbody>());
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Interactable"))
      {
         platformRbs.Remove(other.GetComponent<Rigidbody>());
      }
   }
   
   private void FixedUpdate()
   {
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
   }
}
