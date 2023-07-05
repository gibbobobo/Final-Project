using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFinder : MonoBehaviour
{
    Spawner spawner;
    SpawnControllerSO spawnController;
    List<Transform> waypoints;
    int index = 0;

    private void Awake()
    {
        spawner = GetComponentInParent<Spawner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnController = spawner.GetCurrentWave();
        waypoints = spawnController.GetWaypoints();
        transform.position = waypoints[index].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(index < waypoints.Count)
        {
            Vector3 targetPositon = waypoints[index].position;
            float speed = spawnController.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPositon, speed);
            if(transform.position == targetPositon)
            {
                index++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
