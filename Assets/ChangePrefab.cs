using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePrefab : MonoBehaviour
{
    public GameObject model;
    private void OnEnable()
    {
        HandlePlayerFlowerInteraction.OnCloseToBox += DisplayPrefabChange;
    }

    private void OnDisable()
    {
        HandlePlayerFlowerInteraction.OnCloseToBox -= DisplayPrefabChange;
    }

    private void DisplayPrefabChange(bool show)
    {
        model.SetActive(show);
    }
}
