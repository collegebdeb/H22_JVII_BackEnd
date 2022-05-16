using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CD : MonoBehaviour
{
    public string path;
    void Start()
    {
        Process.Start(path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
