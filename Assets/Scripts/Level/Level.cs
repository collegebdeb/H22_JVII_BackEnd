using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Level : MonoBehaviour
{
    public List<Platform> platforms;
    public GameObject transition;
    public float submergeLevel;
    public AnimationCurve curve;
    public static event Action<Level> OnStartCurrentLevel;

    private void OnEnable()
    {
        Platform.OnPlatformInitialized += AddPlatform;
    }

    private void Start()
    {
        OnStartCurrentLevel?.Invoke(this);
    }

    private void AddPlatform(Platform platform)
    {
        platforms.Add(platform);
    }

    [Button]
    private void Submerge()
    {
        StartCoroutine(CoSubmerge());
    }

    public IEnumerator CoSubmerge()
    {
        float increment = 0;
        float value = 0;
        float blendTime = GameManager.i.levelTransitionTime;
        float rate = 1 / blendTime;
        
        while (increment < 1)
        {
            increment += Time.deltaTime * rate;
            value = curve.Evaluate(increment) * submergeLevel;

            transform.position = new Vector3(transform.position.x, value, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
    }
}
