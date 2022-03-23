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
    private void OnEnable()
    {
        MatrixManager.OnMatrixActivated += StartFilling;
        MatrixManager.OnMatrixDeActivated += StopFilling;
    }

    private void OnDisable()
    {
        MatrixManager.OnMatrixActivated -= StartFilling;
        MatrixManager.OnMatrixDeActivated -= StopFilling;
    }

    private void StartFilling()
    {
        isFillig = true;
    }

    private void StopFilling()
    {
        isFillig = false;
    }

    public void Update()
    {
        if (!isFillig) return;
        print(GameManager.i.currentTimeInMatrix / GameManager.i.maximumTimeInMatrix);
        matrixFillBar.fillAmount = GameManager.i.currentTimeInMatrix / GameManager.i.maximumTimeInMatrix;
    }
}
