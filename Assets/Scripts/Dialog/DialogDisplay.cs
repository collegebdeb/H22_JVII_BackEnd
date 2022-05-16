using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Febucci.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DialogDisplay : MonoBehaviour
{
    private Dialog _dialog;
    public Dialog Dialog
    {
        get => _dialog;
        set => _dialog = value;
    }

    [Required] [SerializeField] private TextMeshProUGUI display;
    [Required] [SerializeField] public TextAnimator textAnimator;
    [Required] [SerializeField] private TextAnimatorPlayer textAnimatorPlayer;
    [Required] [SerializeField] private AudioPeer audioPeer;
    public AudioPeer AudioPeer
    {
        get => audioPeer;
        set => audioPeer = value;
    }

    //When the text of the display is all showed
    public event Action<DialogDisplay> OnTextFinished;
    public event Action<DialogDisplay> OnTextStarted;
    
    //Build the display from the dialog info
    public void ConstructAndDisplayDialog(Dialog dialog)
    {
        Dialog = dialog;
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

    private void AnimateEntryDialog()
    {
        
    }

    IEnumerator CoShowDialog()
    {
        GetComponent<RectTransform>().DOMoveY(145f,0.7f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(1f);
        StartTypeWriter();
    }

    //Set the parameters do tell how to display and animate the text
    private void SetTextAnimatorParameters(Dialog dialog)
    {
       
    }

    //Start typing the text
    private void StartTypeWriter()
    {
        textAnimatorPlayer.StartShowingText();
    }
    
    //Skip the text
    private void SkipTypeWriter()
    {
        textAnimatorPlayer.SkipTypewriter();
    }

    #region AssetUsage for UnityEvents
    
    public void StartShowingText()
    {
        OnTextStarted?.Invoke(this);
    }

    //When the text is done showing, connected to UnityEvent in TextAnimatorPlayer
    public void TextShowed()
    {
        audioPeer._audioSource.clip = null;
        StartCoroutine(CoHideDialog());
    }
    
    IEnumerator CoHideDialog()
    {
        yield return new WaitForSeconds(2.5f);
        GetComponent<RectTransform>().DOMoveY(-145f,0.7f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0.7f);
        OnTextFinished?.Invoke(this);
    }
    

    
    #endregion
}
