using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using Sirenix.OdinInspector;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public AnimationCurve curve;
    public float initialCameraDistance;
    public float finalCameraDistance;

    private void OnEnable()
    {
        LevelExit.OnLevelFinished += StartCameraSequence;
    }
    
    private void OnDisable()
    {
        LevelExit.OnLevelFinished -= StartCameraSequence;
    }

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        initialCameraDistance = cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
    }

    [Button]
    public void StartCameraSequence(Level level, Vector3 pos)
    {
        StartCoroutine(CoCameraSequence());
    }

    public IEnumerator CoCameraSequence()
    {
        CinemachineFramingTransposer transposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        yield return CoMoveCamera(initialCameraDistance, finalCameraDistance);
        yield return new WaitForSeconds(1f);
        yield return CoMoveCamera(finalCameraDistance, initialCameraDistance);
    }

    public IEnumerator CoMoveCamera(float initial, float final)
    {
        CinemachineFramingTransposer transposer = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        
        float increment = 0;
        float value = initial;
        
        float blendTime = GameManager.i.levelTransitionTime/2;
        float rate = 1 / blendTime;
        
        while (increment < 1)
        {
            increment += Time.deltaTime * rate;
            value = initial + curve.Evaluate(increment) * (final-initial);
            RenderSettings.fogStartDistance = value;
            RenderSettings.fogEndDistance = initial + value;
            transposer.m_CameraDistance = value ;
            yield return new WaitForEndOfFrame();
        }
    }
    

}
