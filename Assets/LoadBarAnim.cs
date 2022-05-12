using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBarAnim : MonoBehaviour
{
    public Animator anim;
    public bool isRealCanvas;
    public bool disapering;
    private void OnEnable()
    {
        if (!isRealCanvas) anim.Play("backendbar_Appear");
        else
        {
            MatrixManager.OnStopMatrixPlayingInRealWorld += Hide;
        }
    }

    private void Hide()
    {
        anim.Play("backendbar_Disapear");
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        //MatrixManager.OnStopMatrixPlayingInRealWorld -= Hide;
    }
}
