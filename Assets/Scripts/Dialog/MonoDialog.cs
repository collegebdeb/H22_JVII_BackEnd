using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonoDialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI display;
    public enum DialogState {NotPlaying, Playing}

    public DialogState dialogState;

    public void UpdateNewDisplayText(string newText)
    {
        display.text = newText;
        dialogState = DialogState.Playing;
    }
    
    
}
