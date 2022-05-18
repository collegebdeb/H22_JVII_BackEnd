using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public CanonTrail trail;

    public Transform flowerPos;

    public void PutFlower(Flower flower)
    {
        StartCoroutine(SetFlowerPos(flower));
    }
    public IEnumerator SetFlowerPos(Flower flower)
    {
        flower.transform.DOJump(flowerPos.position,0.5f, 1, 0.5f);
        yield return new WaitForSeconds(1f);
        flower.transform.DOScale(0, 3f);
        yield return new WaitForSeconds(3f);
        trail.changeModel();
        
    }
}
