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
    
    public void LockPlayerControl()
    {
        InputManager.Controls.Disable();
    }
    
    public void UnlockPlayerControl()
    {
        InputManager.Controls.Enable();
    }
}
