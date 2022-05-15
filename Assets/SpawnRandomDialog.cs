using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomDialog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomDialog());

    }

    IEnumerator RandomDialog()
    {
        yield return new WaitForSeconds(2f);
        Dialog dialog = new Dialog("... ... ...");
        dialog.AddInQueue();
    }

    
}
