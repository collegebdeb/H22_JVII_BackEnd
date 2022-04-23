using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public static event Action<Level, Vector3> OnLevelFinished ;
    public GameObject explosion;
    public GameObject visuals;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (MatrixManager.worldState == MatrixManager.WorldState.Matrix) return;
        
        if (other.CompareTag("Player"))
        {
            TriggerVFX();
            InputManager.Controls.Player.Disable();
            InputManager.Controls.Player.Jump.Enable();
            InputManager.Controls.Player.ToggleBackEnd.Disable();
            OnLevelFinished?.Invoke(GameManager.i.currentLevel, transform.position);
            GetComponent<Collider>().enabled = false;
        }
    }

    public void TriggerVFX()
    {
        explosion?.SetActive(true);
        visuals?.SetActive(false);
    }
}
