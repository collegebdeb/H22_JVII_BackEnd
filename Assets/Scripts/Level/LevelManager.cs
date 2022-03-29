using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels;
    private void Awake()
    {
        levels = new List<Level>();
        
        foreach (Transform level in transform)
        {
            levels.Add((level.GetComponent<Level>()));
        }
    }
}
