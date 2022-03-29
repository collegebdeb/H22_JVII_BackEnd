using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class MatrixUIBar : MonoBehaviour
{
    public Image matrixFillBar;
    public bool isFillig;
    public float lastFillAmount;

    private void OnEnable()
    {
        MatrixManager.OnUpdateReverseValue += UpdateMatrixFillAmount;
    }

    public void StartFilling()
    {
        isFillig = true;
    }

    public void StopFilling()
    {
        isFillig = false;
    }

    public void UpdateMatrixFillAmount(float value)
    {
        matrixFillBar.fillAmount = lastFillAmount - value * lastFillAmount;

    }

    public void Update()
    {
        if (isFillig)
        {
            matrixFillBar.fillAmount = GameManager.i.currentTimeInMatrix / GameManager.i.maximumTimeInMatrix;
            lastFillAmount = matrixFillBar.fillAmount;
        }

       
    }
}
