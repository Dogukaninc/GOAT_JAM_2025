using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Curretn level olacak bunun için bir event raise et sonra o evente bağlı olarak o level'ın kapısını aç
    /// </summary>
    public List<Level> levels = new List<Level>();

    public Level currentLevel;

    void Start()
    {
        currentLevel = levels[0];
    }

    void Update()
    {
        if (currentLevel.isLevelFinished)
        {
            currentLevel.onLevelEnd.Raise(this, null);
        }
    }
}