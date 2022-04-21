using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnim : MonoBehaviour
{
    private Animator MenuAnimator;
    // Start is called before the first frame update
    void Start()
    {
        MenuAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MenuAnimator.SetBool("IsTransitionReco", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MenuAnimator.SetBool("IsTransitionReco", false);
        }
    }
}
