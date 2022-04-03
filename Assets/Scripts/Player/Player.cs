using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    
    public enum PlayerType {Real, Matrix}

    [SerializeField] private PlayerType type;
   

    public void SetPlayerType(PlayerType playerType)
    {
        type = playerType;
    }
    
    
 
}
