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
            Vector3 targetDirection = targetPositon - transform.position;
            float speed = spawnController.GetMoveSpeed() * Time.deltaTime;
            //float angle = Vector3.SignedAngle(transform.up, targetDirection, transform.forward); basic working version.
            //transform.Rotate(0.0f, 0.0f, angle-90.0f);
            Quaternion rotation = Quaternion.LookRotation(targetDirection);

            rotation.x = transform.rotation.x;
            rotation.y = transform.rotation.y;

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed);

            Debug.DrawRay(transform.position, targetDirection, Color.red);
            //transform.up = Vector3.RotateTowards(transform.up, targetDirection, speed, 0.0f);
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
