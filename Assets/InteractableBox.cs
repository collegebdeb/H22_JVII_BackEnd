using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBox : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _velocity;
    public enum BoxState{Normal, OnBox, Drag}

    public BoxState state;
    public float gravity;
    public bool grounded;
    public Transform groundCheck;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StickToBoxPlatform.OnBoxConnected += ConnectSelfToPlatformBox;
        StickToBoxPlatform.OnBoxDisconnect += DisconnectSelfToPlatformBox;
    }

    private void ConnectSelfToPlatformBox(Rigidbody platformBoxRb)
    {
        state = BoxState.OnBox;
    }
    
    private void DisconnectSelfToPlatformBox(Rigidbody platformBoxRb)
    {
        if(state == BoxState.OnBox) state = BoxState.Normal;
    }

    private void FixedUpdate()
    {

        switch (state)
        {
            case BoxState.Normal :
                CheckGrounded();
                CalculateGravity();

                break;
            
            case BoxState.OnBox:
                
                break;
            
        }
        
        
        _rb.velocity = _velocity;
    }

    private void CheckGrounded()
    {
        
    }

    private void CalculateGravity()
    {
        if (!grounded)
        {
            float previousYVelocity = _velocity.y;
            float newYVelocity = _velocity.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * (0.5f);
            _velocity.y = nextYVelocity;
        }
        else
        {
            _velocity = new Vector3(0,-0.05f,0);
        }
  
    }
}
