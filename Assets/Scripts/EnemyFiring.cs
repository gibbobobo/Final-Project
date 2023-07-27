using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    Spawner spawner;
    SpawnControllerSO spawnController;
    [SerializeField] GameObject enemyProjectile;

    private void Awake()
    {
        spawner = GetComponentInParent<Spawner>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnController = spawner.GetCurrentWave();
        StartCoroutine(EnemyFire());
    }

    IEnumerator EnemyFire()
    {
        yield return new WaitForSeconds(spawnController.GetAttackDelay());
        while (gameObject)
        {
            yield return new WaitForSeconds(spawnController.GetFireDelay());
            Instantiate(enemyProjectile,
                        transform.position,
                        transform.rotation);
        }
    }
}
