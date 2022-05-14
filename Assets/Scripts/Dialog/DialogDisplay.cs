using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DialogDisplay : MonoBehaviour
{
    private Dialog _dialog;
    
    [Required] [SerializeField] private TextMeshProUGUI display;
    [Required] [SerializeField] public TextAnimator textAnimator;
    [Required] [SerializeField] private TextAnimatorPlayer textAnimatorPlayer;
    
    //When the text of the display is all showed
    public static event Action<DialogDisplay> OnTextShowed;
    
    //Build the display from the dialog info
    public void ConstructAndDisplayDialog(Dialog dialog)
    {
        _dialog = dialog;
        //string[] textt = new string[1] {"size"};
        //if(UnityEngine.Random.Range(0,2) == 0) textAnimator.AssignAppearanceEffects(textt);
        //textAnimator.AssignSharedAppearancesData(dialog.customAppearanceValues);
        textAnimator.SetText(dialog.Content.text, true);
        

        SetTextAnimatorParameters(dialog);

        StartCoroutine(CoShowDialog());
       
   
    }
    
    

    private void OnEnable()
    {
       
    }

    IEnumerator CoShowDialog()
    {
        yield return new WaitForSeconds(2f);
        StartTypeWriter();
        
    }

    private void SetTextAnimatorParameters(Dialog dialog)
    {
        
    }

    private void StartTypeWriter()
    {
        textAnimatorPlayer.StartShowingText();
    }
    
    private void SkipTypeWriter()
    {
        textAnimatorPlayer.SkipTypewriter();
    }

    //When the text is done showing, connected to UnityEvent in TextAnimatorPlayer
    public void TextShowed()
    {
        OnTextShowed?.Invoke(this);
    }
}
