using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class uiLogsEffect : MonoBehaviour
{
    [Title("Inputs")]
    public string logsContent;

    public GameObject logsContainer;
    

    [Title("Dev Inspect")]
    [SerializeField] private int numRow;
    [SerializeField] private TextMeshProUGUI[] logsTextmeshpro;
    [SerializeField] private string[] logsData;
    void Start()
    {
        numRow = logsContainer.transform.childCount;
        
        logsTextmeshpro = new TextMeshProUGUI[numRow];
        logsData = new string[numRow];
        
        for (int i = 0; i < numRow; i++)
        {
            logsTextmeshpro[i] = logsContainer.transform.GetChild(i).transform.GetComponent<TextMeshProUGUI>();
        }
    }
    
    void Update()
    {
        
    }

    private void ShowLogs()
    {

    }
}
