using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogDisplay : MonoBehaviour
{
    private Dialog _dialog;
    [SerializeField] private TextMeshProUGUI display;
    
    //When the text of the display is all showed
    public static event Action<DialogDisplay> OnTextShowed;
    
    //Build the display from the dialog info
    public void ConstructDisplay(Dialog dialog)
    {
        _dialog = dialog;
        display.text = dialog.Content.text;
        print("hello");
    }

    //When the text is done showing, connected to UnityEvent in TextAnimatorPlayer
    public void TextShowed()
    {
        OnTextShowed?.Invoke(this);
    }
}
