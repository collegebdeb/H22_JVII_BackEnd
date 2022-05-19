using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChangePrefab : MonoBehaviour
{
    public GameObject model;
    public GameObject projectileDisplay;
    public Canon _canon;
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

    private void TransferFlower(Canon canon)
    {
        if (canon.gameObject != _canon.gameObject) return;
        projectileDisplay.transform.DOScale(0f, 1f);
    }
    
    private void DisplayPrefabChange(bool show)
    {
        model.SetActive(show);
    }
}
