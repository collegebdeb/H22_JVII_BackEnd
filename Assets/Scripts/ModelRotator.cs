using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ModelRotator : MonoBehaviour
{
    public Vector3 randomValue;
    private void Start()
    {
        randomValue = Random.insideUnitSphere.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(randomValue * Time.deltaTime);
    }
}
