using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{

    public HandlePlayerMovement movement;
    public enum PlayerType {Real, Matrix}

    [SerializeField] private PlayerType type;

    public Transform flowerLockPos;

    private void Awake()
    {
        movement = GetComponent<HandlePlayerMovement>();
    }

    public void SetPlayerType(PlayerType playerType)
    {
        type = playerType;
    }

    public void SetPlayerTypeProperty()
    {
        switch (type)
        {
            case PlayerType.Real:
                
                break;
            case PlayerType.Matrix:
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
 
}
