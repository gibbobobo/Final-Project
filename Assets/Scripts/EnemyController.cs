using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    SpawnController spawnController;
    List<GameObject> waypoints;
    int index = 1;
    [SerializeField] int enemyType;
    [SerializeField] int points;
    [SerializeField] int health;
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] bool largeEnemy;
    [SerializeField] GameObject deathExplosion;
    Coroutine firingCoroutine;
    Coroutine enemyDelay;
    float delay;
    float speed;
    WaypointData waypointData;
    bool delayed = false;
    SpriteRenderer spriteRenderer;
    UIController uiController;

    private void Awake()
    {
        spawnController = GetComponentInParent<SpawnController>();
        waypoints = spawnController.GetWaypoints();
        waypointData = waypoints[index - 1].GetComponent<WaypointData>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        uiController = GameObject.Find("UI Panel").GetComponent<UIController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (index < waypoints.Count)
        {
            MoveEnemy();
            if (waypointData.IsAttacking() && firingCoroutine == null)
            {
                delay = waypointData.GetFireDelay();
                firingCoroutine = StartCoroutine(EnemyFire(delay));
            }
            else if (waypointData.IsAttacking() && firingCoroutine != null && delay != waypointData.GetFireDelay())
            {
                StopCoroutine(firingCoroutine);
                delay = waypointData.GetFireDelay();
                firingCoroutine = StartCoroutine(EnemyFire(delay));
            }
            else if (!waypointData.IsAttacking() && firingCoroutine != null)
            {
                StopCoroutine(firingCoroutine);
                firingCoroutine = null;
            }         
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void MoveEnemy()
    {
        Vector3 targetPositon = waypoints[index].transform.position;
        Quaternion targetRotation = waypoints[index - 1].transform.rotation;
        if (!delayed)
        {
            speed = waypointData.GetMoveSpeed() * Time.deltaTime;
        }
        else
        {
            speed = 0;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed);
        transform.position = Vector2.MoveTowards(transform.position, targetPositon, speed);
        if (transform.position == targetPositon)
        {
            index++;
            waypointData = waypoints[index - 1].GetComponent<WaypointData>();
            if (waypointData.GetMoveDelay() > 0 && enemyDelay == null)
            {
                enemyDelay = StartCoroutine(EnemyDelay());
            }
        }
    }

    IEnumerator EnemyFire(float delay)
    {
        while (gameObject)
        { 
            Instantiate(enemyProjectile,
                        transform.position,
                        transform.rotation);
            if (largeEnemy)
            {
                Instantiate(enemyProjectile,
                       transform.position,
                       Quaternion.Euler(new Vector3(0,0,10f)));
                Instantiate(enemyProjectile,
                       transform.position,
                       Quaternion.Euler(new Vector3(0, 0, -10f)));
            }
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator EnemyDelay()
    {
        delayed = true;
        yield return new WaitForSeconds(waypointData.GetMoveDelay());
        delayed = false;
        enemyDelay = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileController projectile = collision.GetComponent<ProjectileController>();
        
        if (projectile != null && !projectile.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(projectile.GetDamage());
            projectile.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        StartCoroutine(Flash());
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
            uiController.UpdateScore(enemyType, points);
        }
    }

    IEnumerator Flash()
    {
        Color oldColor = spriteRenderer.color;
        Color flashColor = new Color(255, 255, 255, 0.5f);
        for (var i = 0; i < 2; i++)
        {
            spriteRenderer.material.color = flashColor;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.material.color = oldColor;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
