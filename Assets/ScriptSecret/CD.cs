using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Diagnostics;


public class CD : MonoBehaviour
{
    public string path;
    void Start()
    {
        System.Diagnostics.Process.Start("..\batchfilename.bat");
    }
}
