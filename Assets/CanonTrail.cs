using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class CanonTrail : MonoBehaviour
{
    private float ShootDistance
    {
        get => shootDistance;
        set
        {
            shootDistance = value;
            CalculateTargetPos();
        }
    }

    [SerializeField] private float shootDistance;
    public float vanishingPoint;
    public float canonBallInterval;
    public float canonBallSpeed;

    public GameObject Ball;

    private Vector3 targetPos;

    private void OnValidate()
    {
        ShootDistance = shootDistance;
    }

    private void Awake()
    {
        CalculateTargetPos();
    }

    private void CalculateTargetPos()
    {
        Vector3 position = transform.position;
        targetPos = transform.position + transform.forward * shootDistance;
    }

    [Button]
    private void InstantiateBall()
    {
        float ballNumber = ShootDistance / canonBallInterval;

        for (int i = 1; i <= ballNumber; i++)
        {
            var position = transform.position;
            
            Instantiate(Ball, position + transform.forward * canonBallInterval * i, Quaternion.identity);
        }
    }

    private void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,0.4f);
        Gizmos.color = new Color(1f, 0.33f, 0.22f);
        Gizmos.DrawLine(transform.localPosition, targetPos);

        if (transform.hasChanged)
        {
            CalculateTargetPos();
        }
    }

    private void UpdateBallPos()
    {
        
    }

}
