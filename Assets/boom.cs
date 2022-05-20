using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class boom : MonoBehaviour
{
   public GameObject canvas;
   public GameObject canvasCursed;
   
   [Button]
   public void aa()
   {
      canvas.gameObject.SetActive(false);
      canvasCursed.gameObject.SetActive(true);
   }
}
