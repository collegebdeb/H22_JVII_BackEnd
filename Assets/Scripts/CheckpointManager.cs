using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Vector3 checkpoint;
    public MatrixManager matrixManager;

    private void Awake()
    {
       // matrixManager = GetComponent<MatrixManager>();
    }

    private void Start()
    {
        checkpoint += Vector3.up * 2;
    }

    private void OnEnable()
    {
        OnTriggerKillPlayer.OnPlayerDie += HandlePlayerDie;
        OnTriggerKillPlayer.OnPlayerDie += ReloadEntityPosition;
        Projectile.OnCollisionWithPlayer += HandlePlayerDie;
        Projectile.OnCollisionWithPlayer += ReloadEntityPosition;
        LevelExit.OnLevelFinished += RegisterCheckpoint;
    }

    private void ReloadEntityPosition()
    {

        //this line is trash
        foreach (var entities in matrixManager._matrixEntities)
        {
            print("asdasd");
            
            if (entities.CompareTag("Interactable"))
            {
                entities.GetComponent<InteractableBox>().state = InteractableBox.BoxState.Normal;
                entities.GetComponent<InteractableBox>().disAllowBoxSnap = true;
                print("set false");

            }
            
            if (MatrixManager.worldState == MatrixManager.WorldState.Matrix)
            {
                entities.recordedMatrixInfo.Clear();
            } 
            entities.ReloadSelfPosition();
            
           
        }
    }

    private void HandlePlayerDie()
    {
        GameManager.i.playerReal.transform.position = checkpoint;
    }

    private void RegisterCheckpoint(Level level, Vector3 pos)
    {
        checkpoint = pos + Vector3.up * 2;
    }
}
