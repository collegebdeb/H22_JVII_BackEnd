using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCanon : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    { 
        GameObject clone;
        clone = Instantiate(bullet2D, bulletSpawn.position, bulletSpawn.rotation);

    }
}
