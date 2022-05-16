using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CrazyCube : MonoBehaviour
{

    private void Start()
    {
        transform.DOShakePosition(15000f,0.1f);
        transform.DOShakeScale(15000f,0.1f);
        transform.DOShakeRotation(15000f,3f);
    }

}
