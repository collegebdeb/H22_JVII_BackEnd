using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MatrixVisibility : MonoBehaviour
{
    public enum Visibility {RealOnly, MatrixOnly}
    public Visibility visibility;
    
    private void OnEnable()
    {
        UpdateVisibility();
        MatrixManager.OnTriggerToMatrix += UpdateVisibility;
        MatrixManager.OnTriggerToReal += UpdateVisibility;
    }

    public void UpdateVisibility()
    {
        
        
        if (MatrixManager.worldState == MatrixManager.WorldState.Matrix)
        {
            if (visibility == Visibility.MatrixOnly)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (visibility == Visibility.RealOnly)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        } else if (MatrixManager.worldState == MatrixManager.WorldState.Real)
        {
            if (visibility == Visibility.MatrixOnly)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (visibility == Visibility.RealOnly)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        
    }
}
