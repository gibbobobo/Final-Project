using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Config", fileName = "New Enemy Config")]
public class SpawnControllerSO : ScriptableObject
{
    [SerializeField] Transform enemyPath;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float spawnDelay = 1.0f;
    [SerializeField] float waveDelay = 0.0f;
    [SerializeField] float attackDelay = 1f;
    [SerializeField] float fireDelay = 0.5f;
    [SerializeField] List<GameObject> enemyList;
    

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetFireDelay()
    {
        return fireDelay;
    }

    public float GetAttackDelay()
    {
        return attackDelay;
    }

    public Transform GetEnemySpawnPoint()
    {
        return enemyPath.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {    
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in enemyPath)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public int GetEnemyCount()
    {
        return enemyList.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyList[index];
    }

    public float GetSpawnDelay()
    {
        return spawnDelay;
    }

    public float GetWaveDelay()
    {
        return waveDelay;
    }
}
