using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnRandomDialog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomDialog());

    }
    
    public List<SDialog> tuers6;
    public List<SDialog> rate5;
    public List<SDialog> situ2;


    private bool tuer4bool;
    


    private void OnEnable()
    {
        Projectile.OnCollisionWithPlayer += Projectiledialog;
        OnTriggerKillPlayer.OnPlayerDie += Playerdies;
    }

    private void ReadDialog(SDialog sDialog)
    {
        foreach (Dialog dialog in sDialog.dialogs)
        {
            dialog.AddInQueue();
        }
    }

    private void Playerdies()
    {
        ReadDialog(rate5[UnityEngine.Random.Range(0, rate5.Count)]);
    }

    private void Projectiledialog()
    {
        ReadDialog(tuers6[UnityEngine.Random.Range(0, tuers6.Count)]);
    }
    IEnumerator RandomDialog()
    {
        yield return new WaitForSeconds(2f);
        Dialog dialog = new Dialog("... ... ...");
        dialog.AddInQueue();

        while (true)
        {
            yield return new WaitForSeconds(2000f);
            ReadDialog(situ2[UnityEngine.Random.Range(0, situ2.Count)]);
        }
        
    }

    
}
