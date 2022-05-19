using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Febucci.UI;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject canvas;
    public List<string> lists;
    public TextAnimator textAnimator;
    public int index;

    public void Show()
    {
        canvas.SetActive((true));
        
    }

    public void nextdialog()
    {
        StartCoroutine(conext());
    }

    public IEnumerator conext()
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
