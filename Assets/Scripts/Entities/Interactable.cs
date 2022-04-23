using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class Interactable : MonoBehaviour
{
    public List<Transform> raycastPos;
    [SerializeField] private float rayDistance;

    [SerializeField, ReadOnly] private bool isGrounded;
    public Rigidbody rb;

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

    public float highestPosition;
    public Vector3 lastRecordedPosition;
    private void Start()
    {
        highestPosition = transform.position.y;
        lastRecordedPosition = transform.position;
    }
    

    private void FixedUpdate()
    {
        return;
        /**
        if (rb.velocity.y > 0.1f)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, lastRecordedPosition.y, transform.position.z);
            OnStopBoxInteractableVelocity?.Invoke();

        }
        
        if (transform.position.y + rb.velocity.y > transform.position.y + 0.1f)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, lastRecordedPosition.y, transform.position.z);
            OnStopBoxInteractableVelocity?.Invoke();
            
        }
       
        
        RaycastHit hit;

        foreach (Transform ray in raycastPos)
        {
            if(Physics.Raycast(ray.position, ray.up, out hit, rayDistance))
            {
                IsGrounded = true;
                rb.velocity = new Vector3(0, 0, 0);
                return;
            }

            IsGrounded = false;
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        lastRecordedPosition = transform.position;

    **/
    }
    
}
