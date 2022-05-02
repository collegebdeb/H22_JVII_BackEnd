using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGameManager : MonoBehaviour
{
    private Controls controls;
    public CinemachineStateDrivenCamera stateCam;
    private Animator animator;

    private void Awake()
    {
        controls = new Controls();
        animator = stateCam.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controls.MinigameUI.Enable();
        controls.MinigameUI.OpenMenu.performed += OpenMenu;
    }

    private void OpenMenu(InputAction.CallbackContext context)
    {
        print("mmmm");
        animator.SetTrigger("Couch");
    }

}
