using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionTrail : MonoBehaviour
{
    public CanonTrail canonTrail;
    private void Awake()
    {
        canonTrail = GetComponentInParent<CanonTrail>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

