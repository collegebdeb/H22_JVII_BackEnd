using System.Collections;
using System.Collections.Generic;
using devziie.Inputs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlLock : MonoBehaviour
{
    public Image image;
    public Color color;
    public bool lockBeginControl;
    void Start()
    {
        StartCoroutine(OrderAction());
        InputManager.Controls.Player.ToggleBackEnd.Disable();
        if(lockBeginControl) InputManager.Controls.Player.Disable();
        

    }

    IEnumerator OrderAction()
    {
        yield return new WaitForSeconds(1f);
        image.DOColor(color, 3f);
        yield return new WaitForSeconds(9f);
        InputManager.Controls.Player.Move.Enable();
        InputManager.Controls.Player.Jump.Enable();
        InputManager.Controls.Player.Interact.Enable();
        
    }

    // Update is called once per frame
    void Update()
    {
        Gamepad.current.SetMotorSpeeds(Mathf.Sin(Time.time),Mathf.Sin(Time.time));
    }
}
