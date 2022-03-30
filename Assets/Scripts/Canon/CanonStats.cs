using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineEditor]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CanonStats", order = 1)]
public class CanonStats : ScriptableObject
{
    public float canonBallInterval;
    public float canonBallSpeed;
    public int vanishingPointMultiplier = 1;
}
