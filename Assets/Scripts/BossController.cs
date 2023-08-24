using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossController : MonoBehaviour
{
    [SerializeField] int points;
    [SerializeField] int health;
    [SerializeField] int enemyType;
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] float fireDelay;
    [SerializeField] GameObject deathExplosion;
    bool isActive;
    SpriteRenderer spriteRenderer;
    UIController uiController;
    LevelManager levelManager;
    GameObject gameOverText;
    Coroutine firingCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        uiController = GameObject.Find("UI Panel").GetComponent<UIController>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        gameOverText = GameObject.Find("Game Over (TMP)");
    }

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            if (firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(EnemyFire(fireDelay));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileController projectile = collision.GetComponent<ProjectileController>();

        if (projectile != null && !projectile.gameObject.CompareTag("Enemy") && spriteRenderer.enabled)
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
            spriteRenderer.enabled = false;
            StopCoroutine(firingCoroutine);
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
            uiController.UpdateScore(enemyType, points);
            StartCoroutine(GameOver());
            
        }
    }

    IEnumerator Flash()
    {
        Color oldColor = spriteRenderer.color;
        Color flashColor = new Color(255, 255, 255, 0.5f);
        for (var i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.material.color = flashColor;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.material.color = oldColor;
            
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3.0f);
        gameOverText.GetComponent<TextMeshProUGUI>().enabled = true;
        yield return new WaitForSeconds(3.0f);
        levelManager.LoadGameOver();
    }

    public void Activate()
    {
        isActive = true;
    }

    IEnumerator EnemyFire(float delay)
    {
        while (gameObject)
        {
            Instantiate(enemyProjectile,
                        transform.position,
                        Quaternion.identity);
            Instantiate(enemyProjectile,
                       transform.position,
                       Quaternion.Euler(new Vector3(0, 0, 5f)));
            Instantiate(enemyProjectile,
                       transform.position,
                       Quaternion.Euler(new Vector3(0, 0, -5f)));
            yield return new WaitForSeconds(delay);
        }
    }
}
