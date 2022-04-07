using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using devziie.Inputs;

public class LevelManager : MonoBehaviour
{
    [ShowInInspector, SerializeField] public List<Level> levels = new List<Level>();
    public int indexLevel;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Level.OnInitializeLevel += InitializeLevel;
        LevelExit.OnLevelFinished += IncrementIndexLevel;
        LevelExit.OnLevelFinished += StartSubmergeSequence;
    }
    
    private void OnDisable()
    {
        LevelExit.OnLevelFinished -= IncrementIndexLevel;
        LevelExit.OnLevelFinished -= StartSubmergeSequence;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {

        if (levels.Count <= 0) return;
        
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].gameObject.SetActive(false);
        }
        
        levels[0].gameObject.SetActive(true);

    }

    private void InitializeLevel(Level level)
    {
        levels.Add(level);
        //level.gameObject.SetActive(false);
    }

    void IncrementIndexLevel(Level level)
    {
        indexLevel++;
    }

    [Button]
    private void StartRiseSequence(Level level)
    {
        StartCoroutine(CoRiseSequence(level));
    }
    
    IEnumerator CoRiseSequence(Level level)
    {
        yield return new WaitForSeconds(1f);
    }
    
    [Button]
    private void StartSubmergeSequence(Level level)
    {
        StartCoroutine(CoSubmergeSequence(level));
    }

    IEnumerator CoSubmergeSequence(Level level)
    {
        StartCoroutine(CoSubmerge(level, 0, level.submergeLevel));
        level.enabled = false;
        levels[indexLevel].gameObject.SetActive(true);
        yield return CoSubmerge(levels[indexLevel], levels[indexLevel].submergeLevel, 0);
        InputManager.Controls.Player.Enable();
    }
    
    public IEnumerator CoSubmerge(Level level, float initial, float final)
    {
        float increment = 0;
        float value = initial;
        
        float blendTime = GameManager.i.levelTransitionTime;
        float rate = 1 / blendTime;
        
        while (increment < 1)
        {
            increment += Time.deltaTime * rate;
            value = initial + level.curve.Evaluate(increment) * (final-initial);
            level.transform.position = new Vector3(level.transform.position.x, value, level.transform.position.z);
            yield return new WaitForEndOfFrame();
        }
    }
}
