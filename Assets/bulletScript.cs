using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public GameObject John2D;
    public GameObject blackScreen;
    public float speed = 1f; 
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}