using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject John2D;
    public GameObject blackScreen;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(John2D);
            blackScreen.SetActive(true);
            bulletScript.StartShake?.Invoke();
            InputManager.Controls.Player2D.Disable();
        }
    }
}
