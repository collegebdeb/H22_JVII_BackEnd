using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    private float smoothFactor = 0f;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if (target == null) return;
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition,smoothFactor*Time.fixedDeltaTime);
        transform.position = targetPosition;
    }
}


 