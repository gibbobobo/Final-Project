using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] SpawnControllerSO currentWave;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public SpawnControllerSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(currentWave.GetWaveDelay());
        for (int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            Instantiate(currentWave.GetEnemyPrefab(i),
                        currentWave.GetEnemySpawnPoint().position,
                        Quaternion.identity,
                        transform);
            yield return new WaitForSeconds(currentWave.GetSpawnDelay());
        }
    }
}
