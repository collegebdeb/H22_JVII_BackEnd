using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnim : MonoBehaviour
{
    [Range(0, 3)]
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
            
            menuIndexNum++;
            MenuAnimator.SetInteger("MenuIndex", menuIndexNum);
            if (MenuAnimator.GetInteger("MenuIndex") == -1)
            {
                menuIndexNum = 0;
            }
            if (MenuAnimator.GetInteger("MenuIndex") == 4)
            {
                menuIndexNum = 3;
            }


        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
           
            menuIndexNum--;
            MenuAnimator.SetInteger("MenuIndex", menuIndexNum);
           
            if (MenuAnimator.GetInteger("MenuIndex") == 4)
            {
                menuIndexNum = 3;
            }
            if (MenuAnimator.GetInteger("MenuIndex") == -1)
            {
                menuIndexNum = 0;

            }
        }
    }
}
