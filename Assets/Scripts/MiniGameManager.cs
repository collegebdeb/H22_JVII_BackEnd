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
        controls.MinigameUI.Select.performed += OpenMenu;
    }
    
    private void OnDisable()
    {
        controls.MinigameUI.Disable();
        controls.MinigameUI.Select.performed -= OpenMenu;
    }

    private void OpenMenu(InputAction.CallbackContext context)
    {
        StartCoroutine(CoOpenMenu());
       
    }

    private IEnumerator CoOpenMenu()
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("Couch");
    }

}
