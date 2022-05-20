using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI.Core;
using Lean.Pool;
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
        Player.OnLoadQuickSave += ReloadEntityPositionInQuickSave;

    }

    //When dead
    private void ReloadEntityPosition()
    {

        //this line is trash
        foreach (var entities in matrixManager._matrixEntities)
        {

            if (entities.CompareTag("Interactable"))
            {
                entities.GetComponent<InteractableBox>().state = InteractableBox.BoxState.Normal;
                entities.GetComponent<InteractableBox>().disAllowBoxSnap = true;

            }
            
            if (MatrixManager.worldState == MatrixManager.WorldState.Matrix)
            {
                entities.recordedMatrixInfo.Clear();
            } 
            entities.ReloadSelfPosition();
        }
    }

    public GameObject respawn;
    
    private void ReloadEntityPositionInQuickSave()
    {

        //this line is trash
        foreach (var entities in matrixManager._matrixEntities)
        {

            if (entities.CompareTag("Interactable"))
            {
                entities.GetComponent<InteractableBox>().state = InteractableBox.BoxState.Normal;
                entities.GetComponent<InteractableBox>().disAllowBoxSnap = true;

            }
            
            if (MatrixManager.worldState == MatrixManager.WorldState.Matrix)
            {
                entities.recordedMatrixInfo.Clear();
            } 
            entities.ReloadQuickSavePositionToPosition();
        }
    }

    private void HandlePlayerDie()
    {
        GameManager.i.playerReal.transform.position = checkpoint;
        Instantiate(respawn, GameManager.i.playerReal.transform);
    }

    private void RegisterCheckpoint(Level level, Vector3 pos)
    {
        checkpoint = pos + Vector3.up * 2;
    }

 
}
