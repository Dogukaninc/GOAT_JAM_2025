using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();
    public Level currentLevel;
    private int index = 0;
    
    void Start()
    {
        currentLevel = levels[index];
    }

    void Update()
    {
        if (currentLevel.isLevelFinished)
        {
            currentLevel.isLevelFinished = false;
            index++;
            currentLevel=levels[index];
        }
    }
}