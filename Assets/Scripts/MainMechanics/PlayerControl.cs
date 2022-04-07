using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.LowLevel;

[Serializable]
public class PlayerControl
{
    private Player _currentPlayer;
    public PlayerControl(Player realPlayer, Player matrixPlayer)
    {
        this.realPlayer = realPlayer;
        this.matrixPlayer = matrixPlayer;
    }

    public Player matrixPlayer;
    public Player realPlayer;
    public Player CurrentPlayer
    {
        get => _currentPlayer;
        set => _currentPlayer = value;
    }

    public void SetCurrentPlayer(Player player)
    {
        CurrentPlayer = player;
    }

    private Vector3 cachedRealPlayerLocation;
    
    public void SetRealPlayer()
    {

        
        return;
        realPlayer.GetComponent<HandlePlayerMovement>().enabled = false;
        realPlayer.GetComponent<HandlePlayerBoxInteraction>().enabled = false;

        realPlayer.transform.position = cachedRealPlayerLocation;

        matrixPlayer.gameObject.SetActive(false);
        realPlayer.gameObject.SetActive(true);
        
        
        
        matrixPlayer.GetComponent<HandlePlayerBoxInteraction>().enabled = false;
        matrixPlayer.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void ChangePlayerToMatrixState()
    {
        GameManager.i.playerReal.SetPlayerType(Player.PlayerType.Matrix);
        return;
        cachedRealPlayerLocation = realPlayer.transform.position;
        matrixPlayer.transform.position = realPlayer.transform.position;
        
        realPlayer.GetComponent<HandlePlayerMovement>().enabled = true;
        realPlayer.GetComponent<HandlePlayerBoxInteraction>().enabled = true;
        
        realPlayer.gameObject.SetActive(false);
        matrixPlayer.gameObject.SetActive(true);
        
        matrixPlayer.GetComponent<HandlePlayerBoxInteraction>().enabled = true;
        matrixPlayer.GetComponent<CapsuleCollider>().enabled = true;
    }
    
    public void LockPlayerControl()
    {
        InputManager.Controls.Disable();
    }
    
    public void UnlockPlayerControl()
    {
        InputManager.Controls.Enable();
    }
}
