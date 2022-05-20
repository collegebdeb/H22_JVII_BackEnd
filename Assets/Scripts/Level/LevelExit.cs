using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using Sirenix.OdinInspector;
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

    [Button]
    public void Switch()
    {
        TriggerVFX();
        InputManager.Controls.Player.Disable();
        InputManager.Controls.Player.Jump.Enable();
        InputManager.Controls.Player.ToggleBackEnd.Disable();
        OnLevelFinished?.Invoke(GameManager.i.currentLevel, transform.position);
        GetComponent<Collider>().enabled = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (MatrixManager.worldState == MatrixManager.WorldState.Matrix) return;
        if (MatrixManager.worldState == MatrixManager.WorldState.Real)
        {
            if(MatrixManager.isMatrixPlaying) return;
        }
        
        if (other.CompareTag("Player"))
        {
            Switch();
        }
    }

    public void TriggerVFX()
    {
        explosion?.SetActive(true);
        visuals?.SetActive(false);
    }
}
