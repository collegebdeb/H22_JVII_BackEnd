using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InstantCanon : MonoBehaviour
{
    private Transform spawnPos;
    public Projectile projectilePrefab;
    
    
    [Button]
    public void InstantiateBall()
    {
        Instantiate(projectilePrefab, spawnPos.position,Quaternion.identity);
    }
}
