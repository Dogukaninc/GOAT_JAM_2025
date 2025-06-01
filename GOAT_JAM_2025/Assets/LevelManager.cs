using System.Collections.Generic;
using DefaultNamespace;
using Scripts.Utilities;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public Level currentLevel;
    private int index = 0;

    public TextMeshProUGUI countText;

    void Start()
    {
        currentLevel = levels[index];
    }

    private void UpdateCountText()
    {
        countText.text = $"{GeneralValuesHolder.Instance.Player.LightSeams.Count}/{currentLevel.desiredLightCount}";
    }

    void Update()
    {
        if (currentLevel.isLevelFinished)
        {
            currentLevel.isLevelFinished = false;
            index++;
            currentLevel = levels[index];
        }

        UpdateCountText();
    }
}