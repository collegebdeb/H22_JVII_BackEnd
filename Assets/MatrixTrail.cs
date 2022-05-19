using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTrail : MonoBehaviour
{
    public GameObject matrixTrail;
    // Update is called once per frame
    void Update()
    {
        switch (MatrixManager.worldState)
        {
            case MatrixManager.WorldState.Real:
                if (MatrixManager.isMatrixPlaying)
                {
                    matrixTrail.SetActive(true);
                } else
                {
                    matrixTrail.SetActive(false);
                }
             
                break;
            case MatrixManager.WorldState.TransitioningToReal:
                matrixTrail.SetActive(false);
                break;
            case MatrixManager.WorldState.Matrix:
                matrixTrail.SetActive(true);
                transform.position = GameManager.i.playerReal.transform.position;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }
}
