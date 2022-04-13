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

    public static event Action OnFinishedLevelSubmerge;
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
        GameManager.i.SetCurrentLevel(levels[0]);
    }

    private void InitializeLevel(Level level)
    {
        levels.Add(level);
        //level.gameObject.SetActive(false);
    }

    void IncrementIndexLevel(Level level, Vector3 pos)
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
    private void StartSubmergeSequence(Level level, Vector3 pos)
    {
        StartCoroutine(CoSubmergeSequence(level));
    }

    IEnumerator CoSubmergeSequence(Level level)
    {
        print(level.gameObject.name);
        StartCoroutine(CoSubmerge(level, 0, level.submergeLevel)); // Submerge in water
        levels[indexLevel].gameObject.SetActive(true);
        GameManager.i.SetCurrentLevel(levels[indexLevel]);
        yield return CoSubmerge(levels[indexLevel], levels[indexLevel].submergeLevel, 0); //Rise
        InputManager.Controls.Player.Enable();
        InputManager.Controls.Player.ToggleBackEnd.Enable();
        OnFinishedLevelSubmerge?.Invoke();
        
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
