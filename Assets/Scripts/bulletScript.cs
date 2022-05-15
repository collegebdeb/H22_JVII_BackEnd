using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public GameObject John2D;
    public GameObject blackScreen;
    public float speed = 1f; 
    // Start is called before the first frame update

    public static Action StartShake;
    void Start()
    {
        John2D = GameObject.Find("JohnPngV2");
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(John2D);
            blackScreen.SetActive(true);
            StartShake?.Invoke();
            InputManager.Controls.Player2D.Disable();
        }
    }
}