using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class CollisionCanon : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet2D;

    public GameObject blackScreen;
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
        clone.GetComponent<bulletScript>().blackScreen = blackScreen;
        MasterAudio.PlaySound("Sfx_RetroCanon");
        MasterAudio.MuteGroup("MenuST_01");
        MasterAudio.PlaySound("MenuST_02");

    }
}
