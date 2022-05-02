using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InstantCanon : MonoBehaviour
{
    public Transform spawnPos;
    public InstantProjectile projectilePrefab;
    
    
    [Button]
    public void InstantiateBall()
    {
        Instantiate(projectilePrefab, spawnPos.position,Quaternion.identity);
    }

    private void Start()
    {
        StartCoroutine(ShootBall());
    }

    private IEnumerator ShootBall()
    {
        while (true)
        {
            InstantiateBall();
            yield return new WaitForSeconds(1f);
            
        }
        
        
    }
}
