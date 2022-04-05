using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.Collections;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public List<Transform> raycastPos;
    [SerializeField] private float rayDistance;

    [SerializeField, ReadOnly] private bool isGrounded;

    public static event Action<bool> IsGroundedChanged;
    public bool IsGrounded
    {
        get => isGrounded;
        set
        {
            if(isGrounded!=value) IsGroundedChanged?.Invoke(isGrounded);
            isGrounded = value;
        }
    }


    private void OnDrawGizmos()
    {
        foreach (Transform ray in raycastPos)
        {
            Gizmos.DrawLine(ray.position, ray.position + ray.transform.up * rayDistance);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        foreach (Transform ray in raycastPos)
        {
            if(Physics.Raycast(ray.position, ray.up, out hit, rayDistance))
            {
                IsGrounded = true;
                return;
            }

            IsGrounded = false;
        }

    }
}
