using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using devziie.Inputs;
using UnityEngine.InputSystem;

public class MatrixManager : MonoBehaviour
{

    public enum World
    {
        Real,
        Matrix
    }

    public World worldType;

    public int maximumTimeInMatrix;
    private int currentTimer;
    
    bool isMatrixRecordingPlaying;
    public void OnEnable()
    {
        InputManager.Controls.Player.ToggleBackEnd.started += OnToggleBackEnd;
    }
    
    public void OnDisable()
    {
        InputManager.Controls.Player.ToggleBackEnd.started -= OnToggleBackEnd;
    }

    private void OnToggleBackEnd(InputAction.CallbackContext context)
    {
        if (worldType == World.Real)
        {
            if (isMatrixRecordingPlaying)
            {
                SoundEvents.onCannotSwitchToMatrix?.Invoke();
                return;
            }
        } else if (worldType == World.Matrix)
        {
            
        }
        
    }

   
}
