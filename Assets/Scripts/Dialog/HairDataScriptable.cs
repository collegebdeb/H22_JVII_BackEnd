using System.Collections;
using System.Collections.Generic;
using Febucci.UI.Core;
using UnityEngine;

[CreateAssetMenu(fileName = "Parameters", menuName = "Dialog/hair", order = 100)]
public class HairDataScriptable<Dialog> : ScriptableObject where Dialog : new()
{
    [SerializeField] public Dialog effectValues = new Dialog();
}
