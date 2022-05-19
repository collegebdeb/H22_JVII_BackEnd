using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas realCanvas;
    public Canvas matrixCanvas;
    public GameObject Logs;


    public MatrixUIBar matrixUIBar;
    private void OnEnable()
    {
        MatrixManager.OnRealWorldActivated += DisplayRealWorldActivatedCanvas;
        MatrixManager.OnMatrixActivated += DisplayMatrixActivatedCanvas;
        MatrixManager.OnTransitionActivated += UpdateDisplayTransition;
    }

    public LoadBarAnim matrixBar;
    public LoadBarAnim realBar;

    private void Update()
    {
        if (MatrixManager.worldState == MatrixManager.WorldState.Matrix)
        {
            Logs.SetActive(true);
            matrixCanvas.gameObject.SetActive(true);
        }
        else
        {
            Logs.SetActive(false);

            if (MatrixManager.isMatrixPlaying)
            {
                matrixCanvas.gameObject.SetActive(true);
            }
            else
            {
                matrixCanvas.gameObject.SetActive(false);
            }
        }
        
        if (MatrixManager.isMatrixPlaying)
        {
            realBar.gameObject.SetActive(true);
        }
        else
        {
        //    realBar.anim.Play("backendbar_Disapear");
            realBar.gameObject.SetActive(false);
        }

    }

    private void Start()
    {
        DisplayRealWorldActivatedCanvas();
    }

    #region UI Canvas

    public void DisplayRealWorldActivatedCanvas()
    {
        matrixUIBar.StopFilling();
        DisableAllCanvas();
        realCanvas.gameObject.SetActive(true);
    }
    
    public void UpdateDisplayTransition()
    {
        matrixUIBar.StopFilling();
    }
    
    public void DisplayMatrixActivatedCanvas()
    {
        matrixUIBar.StartFilling();
        DisableAllCanvas();
        matrixCanvas.gameObject.SetActive(true);
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
