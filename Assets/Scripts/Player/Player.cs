using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using devziie.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class Player : MonoBehaviour
{

    public HandlePlayerMovement movement;
    public enum PlayerType {Real, Matrix}

    [SerializeField] private PlayerType type;

    public Transform flowerLockPos;
    public Transform flowerDropPos;

    public static event Action OnQuickSave;
    public static event Action OnLoadQuickSave;

    private void Awake()
    {
        movement = GetComponent<HandlePlayerMovement>();
    }

    private void OnEnable()
    {
        InputManager.Controls.Player.QuickSave.performed += QuickSave;
        InputManager.Controls.Player.LoadQuickSave.performed += LoadQuickSave;
    }

    private void OnDisable()
    {
        InputManager.Controls.Player.QuickSave.performed -= QuickSave;
        InputManager.Controls.Player.LoadQuickSave.performed -= LoadQuickSave;
    }

    private void QuickSave(InputAction.CallbackContext context)
    {
        OnQuickSave?.Invoke();
        GameObject.Find("Checkpoint").transform.position = GameManager.i.playerReal.transform.position;
    }
    
    private void LoadQuickSave(InputAction.CallbackContext context)
    {
        OnLoadQuickSave?.Invoke();
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
