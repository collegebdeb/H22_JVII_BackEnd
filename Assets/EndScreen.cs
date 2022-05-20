using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DarkTonic.MasterAudio;
using devziie.Inputs;
using Febucci.UI;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject canvas;
    public List<string> lists;
    public TextAnimator textAnimator;
    public int index;
    public AudioSource source2;
    public AudioSource source;
    public void Show()
    {
        source.Play();
        source2.Play();
        MasterAudio.MuteEverything();
        canvas.SetActive((true));
        nextdialog();
        InputManager.Controls.Player.Disable();
    }
    
    

    public void nextdialog()
    {
        StartCoroutine(conext());
    }
    
    

    public IEnumerator conext()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            textAnimator.SetText(lists[index],false);
            index++;
        
            if (index >= lists.Count-1)
            {
                Application.Quit();
            }
        }
       
    }
}
