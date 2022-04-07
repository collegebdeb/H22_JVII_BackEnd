using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerKillPlayer : MonoBehaviour
{
   public static event Action OnPlayerDie;
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         OnPlayerDie?.Invoke();
      }
   }
}
