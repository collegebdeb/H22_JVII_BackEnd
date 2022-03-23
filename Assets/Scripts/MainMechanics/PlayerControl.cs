using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using UnityEngine;
using UnityEngine.LowLevel;

[Serializable]
public class PlayerControl
{
    private Player _currentPlayer;
    public PlayerControl(Player player1, Player player2)
    {
        
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
    
    public void LockPlayerControl()
    {
        InputManager.Controls.Disable();
    }
    
    public void UnlockPlayerControl()
    {
        InputManager.Controls.Enable();
    }
}
