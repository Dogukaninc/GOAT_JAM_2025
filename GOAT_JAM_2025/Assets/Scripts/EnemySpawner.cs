using NUnit.Framework;
using System.Collections;
using Scripts.Utilities;
using UnityEngine;

public class EnemySpawner : SingletonMonoBehaviour<EnemySpawner>
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int[] numberOfEnemies;
    [SerializeField] private GameObject vfx;
    private int currentStage = 0;
    private int spawnedEnemies = 0;

    private void Start()
    {
        onEnterNextStage();
    }

    public void onEnterNextStage()
    {
        StartCoroutine(SpawnEnemy());
    }
    
    public void OnStageChange()//TODO: Yeni Levela geçerken çağır kapıda
    {
        currentStage++;
        spawnedEnemies = 0;
        onEnterNextStage();
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2);
        while (spawnedEnemies < numberOfEnemies[currentStage])
        {
            Transform spawnTarget = transform.GetChild(currentStage).transform.GetChild(Random.Range(0, transform.GetChild(currentStage).transform.childCount));
            GameObject x = Instantiate(vfx, spawnTarget.position,spawnTarget.rotation);
            yield return new WaitForSeconds(2);
            Destroy(x);
            Instantiate(enemy,spawnTarget.position,spawnTarget.rotation);
            spawnedEnemies++;
            yield return new WaitForSeconds(Random.Range(4f,10f));
        }
    }
}
