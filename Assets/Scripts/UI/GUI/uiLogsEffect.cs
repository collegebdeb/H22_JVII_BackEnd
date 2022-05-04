using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class uiLogsEffect : MonoBehaviour
{
    [Title("Inputs")]
    public GameObject logsContainer;
    
    private int _numRow;

    private TextMeshProUGUI[] _logsTextmeshpro;
    
    private string _logsInput = "";
    private string _lastLogs;

    void Start()
    {
        _numRow = logsContainer.transform.childCount;
        
        _logsTextmeshpro = new TextMeshProUGUI[_numRow];

        for (int i = 0; i < (_numRow); i++)
        {
            _logsTextmeshpro[i] = logsContainer.transform.GetChild(i).transform.GetComponent<TextMeshProUGUI>();
            _logsTextmeshpro[i].text = "";
        }
    }
    private void AfficherLogs()
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
    public void ImportLogs(string unityLogs)
    {
        _logsInput = unityLogs;
        AfficherLogs();
        
    }
}
