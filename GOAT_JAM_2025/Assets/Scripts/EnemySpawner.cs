using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnPoints;
    [SerializeField] private int[] numberOfEnemies;
    private int currentStage = 0;
    private int spawnedEnemies = 0;

        
    public void onEnterNextStage()
    {
        SpawnEnemy();
    }
    
    public void OnStageChange()
    {
        currentStage++;
        spawnedEnemies = 0;
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2);

        while (spawnedEnemies <= numberOfEnemies[currentStage])
        {
            Instantiate(enemy, spawnPoints.GetChild(currentStage).transform.GetChild((int)Random.Range(0,7)));
            spawnedEnemies++;
            yield return new WaitForSeconds(Random.Range(1f,4f));
        }
    }
}
