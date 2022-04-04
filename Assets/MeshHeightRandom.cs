using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHeightRandom : MonoBehaviour
{
    public Transform realWorld;
    public Transform matrixWorld;
    public float range;
    
    void Start()
    {
        return;
        gameObject.isStatic = false;
        if (Random.Range(0, 2) != 0 || transform.position.y != 0) return;
        float random = Random.Range(-range, range);
        realWorld.position = new Vector3(transform.position.x, transform.position.y + random, transform.position.z);
        matrixWorld.position = new Vector3(transform.position.x, transform.position.y + random, transform.position.z);
        gameObject.isStatic = true;
    }

   
}
