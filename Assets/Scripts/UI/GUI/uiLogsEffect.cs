using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System.Linq;

public class uiLogsEffect : MonoBehaviour
{
    public static Action<string> Send;

    public List<string> RandomLogsCues;
    
    [MinMaxSlider(5,15)]
    public Vector2 intervalBetweenRandomCues;

    private float timer = 30f;
    private int index;
    private void Update()
    {
       
    }
    
    private void OnEnable()
    {
        Send += ImportLogs;
    }

    [Title("Inputs")]
    public static GameObject logsContainer;

    private static int _numRow;

    private static TextMeshProUGUI[] _logsTextmeshpro;
    
    private static string _logsInput = "";
    private static string _lastLogs;

    IEnumerator Start()
    {
        RandomLogsCues = RandomLogsCues.OrderBy(a => Guid.NewGuid()).ToList();
        VerticalLayoutGroup layout = GetComponentInChildren<VerticalLayoutGroup>();
        logsContainer = layout.gameObject;
        _numRow = logsContainer.transform.childCount;
        
        _logsTextmeshpro = new TextMeshProUGUI[_numRow];

        for (int i = 0; i < (_numRow); i++)
        {
            _logsTextmeshpro[i] = logsContainer.transform.GetChild(i).transform.GetComponent<TextMeshProUGUI>();
            _logsTextmeshpro[i].text = "";
        }

        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(intervalBetweenRandomCues.x, intervalBetweenRandomCues.y));
            
            if (index > RandomLogsCues.Count-1)
            {
                RandomLogsCues = RandomLogsCues.OrderBy(a => Guid.NewGuid()).ToList();
                index = 0;
            }
          
            
            ImportLogs(RandomLogsCues[index]);
            index++;
            
        }
        
    }
    private static void AfficherLogs()
    {
        if (_logsInput != _lastLogs)
        {
            for (int i = 0; i < (_numRow-1); i++)
            {
                _logsTextmeshpro[(_numRow - i) - 1].text = _logsTextmeshpro[(_numRow - i)-2].text;
            }

            _logsTextmeshpro[0].text = _logsInput;
            _lastLogs = _logsInput;
        }
    }
    [Button]
    public static void ImportLogs(string unityLogs)
    {
        _logsInput = unityLogs;
        AfficherLogs();
        
    }
}
