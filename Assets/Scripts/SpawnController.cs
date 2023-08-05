using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnController : MonoBehaviour
{
    [SerializeField] GameObject enemyPath;
    [SerializeField] float spawnDelay = 1.0f;
    [SerializeField] float waveDelay = 0.0f;
    [SerializeField] List<GameObject> enemyList;

    Transform spawnPoint;

    public List<GameObject> GetWaypoints()
    {    
        List<GameObject> waypoints = new List<GameObject>();
        foreach (Transform child in enemyPath.transform)
        {
            waypoints.Add(child.gameObject);
        }
        return waypoints;
    }

    private void Awake()
    {
        spawnPoint = enemyPath.transform.GetChild(0).transform;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(waveDelay);
        for (int i = 0; i < enemyList.Count; i++)
        {
            Instantiate(enemyList[i],
                        spawnPoint.position,
                        spawnPoint.rotation,
                        transform);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
