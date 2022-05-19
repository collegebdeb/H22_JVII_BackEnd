using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChangePrefab : MonoBehaviour
{
    public GameObject model;
    public GameObject projectileDisplay;
    private void OnEnable()
    {
        HandlePlayerFlowerInteraction.OnCloseToBox += DisplayPrefabChange;
        HandlePlayerFlowerInteraction.OnTransferFlower += TransferFlower;
    }

    private void OnDisable()
    {
        HandlePlayerFlowerInteraction.OnCloseToBox -= DisplayPrefabChange;
        HandlePlayerFlowerInteraction.OnTransferFlower -= TransferFlower;
    }

    private void TransferFlower()
    {
        projectileDisplay.transform.DOScale(0f, 1f);
    }
    
    private void DisplayPrefabChange(bool show)
    {
        model.SetActive(show);
    }
}
