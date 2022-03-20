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
        MatrixManager.OnTriggerToReal += DisplayRealCanvas;
        MatrixManager.OnTriggerToMatrix += DisplayMatrixCanvas;
    }

    #region UI Canvas

    public void DisplayRealCanvas()
    {
        DisableAllCanvas();
        realCanvas.gameObject.SetActive(true);
    }
    
    public void DisplayMatrixCanvas()
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
        realCanvas.gameObject.SetActive(false);
        matrixCanvas.gameObject.SetActive(false);
        transitionToMatrixCanvas.gameObject.SetActive(false);
        transitionToRealCanvas.gameObject.SetActive(false);
    }

    #endregion
}
