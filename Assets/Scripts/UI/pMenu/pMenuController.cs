using System;
using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using DarkTonic.MasterAudio;

public class pMenuController : MonoBehaviour
{
    private int _numBoutons;
    private pMenuBouton[] _boutons;

    [SerializeField] private string _upButton;
    [SerializeField] private string _downButton;
    [SerializeField] private string _selectButton;

    private int _indexSelection;

    public void DisapearSelf()
    {
        gameObject.SetActive(false);
    }

    private enum Direction
    {
        up,
        down
    };

    private void OnEnable()
    {
        InputManager.Controls.MinigameUI.Enable();
        
        InputManager.Controls.MinigameUI.Down.performed += ClickOnDown;
        InputManager.Controls.MinigameUI.Up.performed += ClickOnUp;
        InputManager.Controls.MinigameUI.Select.performed += Select;
    }
    
    private void OnDisable()
    {
        InputManager.Controls.MinigameUI.Down.performed -= ClickOnDown;
        InputManager.Controls.MinigameUI.Up.performed -= ClickOnUp;
        InputManager.Controls.MinigameUI.Select.performed -= Select;
    }

    private void ClickOnDown(InputAction.CallbackContext context)
    {
        IndexSelection(Direction.down);
        MasterAudio.PlaySound("Sfx_UI_OnHoverTick_01");
    }

    private void ClickOnUp(InputAction.CallbackContext context)
    {
        IndexSelection(Direction.up);
        MasterAudio.PlaySound("Sfx_UI_OnHoverTick_01");
    }

    private void Select(InputAction.CallbackContext context)
    {
        _boutons[_indexSelection].TriggerButton();
    }

    public void jouer()
    {
        MasterAudio.PlaySound("Sfx_UI_OnPressStart_01");
        MasterAudio.PlaySound("MenuST_01");
    }
    public void Quitter()
    {
        MasterAudio.PlaySound("Sfx_UI_OnPressExit_01");
    }

    void Start()
    {
        _numBoutons = gameObject.transform.childCount;
        _boutons = new pMenuBouton[_numBoutons];

        for (int i = 0; i < (_numBoutons); i++)
        {
            _boutons[i] = transform.GetChild(i).gameObject.GetComponent<pMenuBouton>();
        }
        
        _boutons[0].ChangeStatut(true);
        
        
    }
    
    private void IndexSelection(Direction direction)
    {
        _boutons[_indexSelection].ChangeStatut(false);
        if (direction == Direction.down)
        {
            if (_indexSelection < (_boutons.Length - 1))
            {
                _indexSelection++;
            }
            else if (_indexSelection == (_boutons.Length - 1))
            {
                _indexSelection = 0;
            }
        }
        else if (direction == Direction.up)
        {
            if (_indexSelection == 0)
            {
                _indexSelection = _boutons.Length - 1;
            }
            else if (_indexSelection > 0)
            {
                _indexSelection--;
            }
        }
        _boutons[_indexSelection].ChangeStatut(true);
    }
}
