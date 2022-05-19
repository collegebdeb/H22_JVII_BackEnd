using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DarkTonic.MasterAudio;
using devziie.Inputs;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Couchcam : MonoBehaviour
{
    public Image image;
    private void OnEnable()
    {
        bulletScript.StartShake += StartShaking;
    }

    public UnityEvent Shake;
    public UnityEvent ShakeStop;

    [Button]
    private void StartShaking()
    {
        InputManager.Controls.Player2D.Disable();
        InputManager.Controls.MinigameUI.Disable();
        print("starthsaking");
        Shake.Invoke();
        StartCoroutine(ChangeScene());
        StartCoroutine(ChangeOh());
        Gamepad.current.SetMotorSpeeds(1,1);
    }
    
    [Button]
    private void Stop()
    {
        CinemachineImpulseManager.Instance.Clear();
    }

    public IEnumerator ChangeScene()
    {
        MasterAudio.PlaySound("Sfx_Mai_DeathTransition_01");
        MasterAudio.MuteGroup("MenuST_01");
        MasterAudio.MuteGroup("MenuST_02");
        yield return new WaitForSeconds(9f);
        Gamepad.current.SetMotorSpeeds(0,0);
        SceneManager.LoadScene(1);
    }

    public Color colorr;
    public IEnumerator ChangeOh()
    {
        
        yield return new WaitForSeconds(4.6f);
        image.DOColor(colorr,4f);

    }
}
