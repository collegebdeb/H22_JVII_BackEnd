using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnim : MonoBehaviour
{
    private int menuIndexNum;
    private Animator MenuAnimator;
    // Start is called before the first frame update
    void Start()
    {
        MenuAnimator = GetComponent<Animator>();
        menuIndexNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (menuIndexNum > 4)
            {
                menuIndexNum = 4;
            }
            menuIndexNum++;
            MenuAnimator.SetInteger("MenuIndex", menuIndexNum);
           
            
           
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (menuIndexNum < 0)
            {
                menuIndexNum = 0;
            }
            menuIndexNum--;
            MenuAnimator.SetInteger("MenuIndex", menuIndexNum);
            
            
        }
    }
}
