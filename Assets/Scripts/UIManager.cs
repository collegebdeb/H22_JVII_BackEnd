using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas realCanvas;
    public Canvas matrixCanvas;
    public Canvas transitionToMatrixCanvas;
    public Canvas transitionToRealCanvas;

    private void OnEnable()
    {
        MatrixManager.OnRealWorldActivated += DisplayRealWorldActivatedCanvas;
        MatrixManager.OnMatrixActivated += DisplayMatrixActivatedCanvas;
    }

    private void Start()
    {
        DisplayRealWorldActivatedCanvas();
    }

    #region UI Canvas

    public void DisplayRealWorldActivatedCanvas()
    {
        DisableAllCanvas();
        realCanvas.gameObject.SetActive(true);
    }
    
    public void DisplayMatrixActivatedCanvas()
    {
        DisableAllCanvas();
        matrixCanvas.gameObject.SetActive(true);
    }
    
    public void DisplayTransitionToMatrixCanvas()
    {
        DisableAllCanvas();
        transitionToMatrixCanvas.gameObject.SetActive(true);
    }
    
    public void DisplayTransitionToRealCanvas()
    {
        DisableAllCanvas();
        transitionToRealCanvas.gameObject.SetActive(true);
    }
    public void DisableAllCanvas()
    {
        realCanvas?.gameObject.SetActive(false);
        matrixCanvas?.gameObject.SetActive(false);
        //transitionToMatrixCanvas.gameObject.SetActive(false);
        //transitionToRealCanvas?.gameObject.SetActive(false);
    }

    #endregion
}
