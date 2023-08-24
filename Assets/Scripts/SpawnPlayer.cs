using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject playerShip;
    [SerializeField] GameObject spawnPoint;
    Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        //spawnPos = Camera.main.ScreenToWorldPoint(spawnPoint.transform.position);
        Instantiate(playerShip, spawnPoint.transform.position, Quaternion.identity);
    }
}
