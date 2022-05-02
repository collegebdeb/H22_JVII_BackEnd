using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InstantProjectile : MonoBehaviour
{
    private void Update()
    {
        if (MatrixManager.worldState == MatrixManager.WorldState.Real && MatrixManager.isMatrixPlaying) return;
        if (MatrixManager.worldState == MatrixManager.WorldState.TransitioningToReal) return;
        transform.position += -transform.forward * 2f * Time.deltaTime;
    }
}
