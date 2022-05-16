using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerKillPlayer : MonoBehaviour
{
   public ParticleSystem waterSplash;
   public static event Action OnPlayerDie;
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         Lean.Pool.LeanPool.Spawn(waterSplash, other.transform.position + new Vector3(0,1f,0), waterSplash.transform.rotation);
         OnPlayerDie?.Invoke();
         
      }
   }
}
