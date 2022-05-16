using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Diagnostics;
using Sirenix.OdinInspector;


public class CD : MonoBehaviour
{
    [Button]
    void Crash()
    {
        System.Diagnostics.Process.Start("Assets/ScriptSecret/crash.bat");
    }
}
