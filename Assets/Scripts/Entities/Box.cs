using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.PlayerLoop;

[SelectionBase]
public class Box : Interactable
{
    public enum State {Idle, PushedByPlayer}
    public State state;
    
    public Rigidbody rb;
    
    
    private void FixedUpdate()
    {
      
      //  rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime);

    }

    private void Update(){
}
}
