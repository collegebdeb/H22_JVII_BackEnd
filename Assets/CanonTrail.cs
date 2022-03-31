using System;
using System.Collections;
using System.Collections.Generic;
using DG.DemiLib;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine.Serialization;

public class CanonTrail : MonoBehaviour
{
    public CanonStats stats;
    public List<Projectile> projectilePrefabs;
    private float ShootVanishPoint
    {
        get => vanishDistance;
        set
        {
            vanishDistance = value;
            CalculateTargetPos();
        }
    }

    [SerializeField] private float vanishDistance;
    [SerializeField, ReadOnly] private float shootDistanceLoop;
    [SerializeField, ReadOnly] private float vanishLoopRange;
    
    public GameObject projectilePrefab;

    private Vector3 targetPos;

    private void OnValidate()
    {
        ShootVanishPoint = vanishDistance;
    }

    private void Awake()
    {
        CalculateTargetPos();
    }

    private void CalculateTargetPos()
    {
        Vector3 position = transform.position;
        targetPos = transform.position + transform.forward * vanishDistance;
    }

    private void AutomaticAdjustShootDistance()
    {
        vanishDistance -= vanishDistance % stats.canonBallInterval;
        shootDistanceLoop = vanishDistance + stats.canonBallInterval * stats.vanishingPointMultiplier;
        vanishLoopRange = shootDistanceLoop - vanishDistance;
    }
    
    [Button]
    private void InstantiateBall()
    {
        AutomaticAdjustShootDistance();
        int ballNumber = (int) (shootDistanceLoop / stats.canonBallInterval);

        for (int i = 0; i < ballNumber; i++)
        {
            var position = transform.position;
            Projectile instance = Instantiate(projectilePrefab, position + transform.forward * stats.canonBallInterval * i, Quaternion.identity).GetComponent<Projectile>();
            instance.transform.SetParent(transform);
            projectilePrefabs.Add(instance);
        }
    }

    private void Update()
    {
        UpdateBallPos();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.33f, 0.22f);
        Gizmos.DrawLine(transform.position, targetPos);

        if (transform.hasChanged)
        {
            CalculateTargetPos();
        }
    }

    public float alphaValue;
    private void UpdateBallPos()
    {
        foreach (var instance in projectilePrefabs)
        {
            var position = instance.transform.position;
            instance.transform.position = position + transform.forward * Time.unscaledDeltaTime * stats.canonBallSpeed;
            
            if (instance.transform.localPosition.z > shootDistanceLoop)
            {
                instance.transform.position = transform.position;
                instance.ResetLoop();
            }
            
            if (instance.transform.localPosition.z > ShootVanishPoint)
            {
                alphaValue = Mathf.Lerp(1, 0, (instance.transform.localPosition.z - vanishDistance) / vanishLoopRange);
                instance.GetComponent<Renderer>().material.color= new Color(1f, 1f, 1f, alphaValue);
            }
            else
            {
                instance.GetComponent<Renderer>().material.color = Color.white;
            }

            
        }
    }

}
