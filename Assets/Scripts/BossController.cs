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
    [SerializeField] List<GameObject> tentacles;
    public bool isActive;
    SpriteRenderer spriteRenderer;
    SpriteRenderer spriteRendererClaw1;
    SpriteRenderer spriteRendererClaw2;
    UIController uiController;
    LevelManager levelManager;
    GameObject victoryText;
    Coroutine firingCoroutine;
    Coroutine tentacleCoroutine;
    int tentacleIndex;

    private void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        spriteRendererClaw1 = GameObject.Find("LegTop").GetComponent<SpriteRenderer>();
        spriteRendererClaw2 = GameObject.Find("LegBottom").GetComponent<SpriteRenderer>();
        uiController = GameObject.Find("UI Panel").GetComponent<UIController>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        victoryText = GameObject.Find("Victory (TMP)");
    }

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        tentacleIndex = 0;
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
            if (tentacleCoroutine == null)
            {
                tentacleCoroutine = StartCoroutine(TentacleAttack());
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
            spriteRendererClaw1.enabled = false;
            spriteRendererClaw2.enabled = false;
            isActive = false;
            StopCoroutine(firingCoroutine);
            Instantiate(deathExplosion, transform.position, Quaternion.identity, transform.parent);
            uiController.UpdateScore(enemyType, points);
            StartCoroutine(Victory());       
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

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(3.0f);
        victoryText.GetComponent<TextMeshProUGUI>().enabled = true;
        yield return new WaitForSeconds(3.0f);
        TitleScreen.gameOver = true;
        levelManager.LoadMainMenu();
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
                       Quaternion.Euler(new Vector3(0, 0, 4f)));
            Instantiate(enemyProjectile,
                       transform.position,
                       Quaternion.Euler(new Vector3(0, 0, -4f)));
            Instantiate(enemyProjectile,
                       transform.position,
                       Quaternion.Euler(new Vector3(0, 0, 8f)));
            Instantiate(enemyProjectile,
                       transform.position,
                       Quaternion.Euler(new Vector3(0, 0, -8f)));
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator TentacleAttack()
    {
        Tentaclecontroller tentacle1 = tentacles[tentacleIndex].GetComponent<Tentaclecontroller>();
        tentacle1.Activate();
        Tentaclecontroller tentacle2 = tentacles[tentacleIndex+3].GetComponent<Tentaclecontroller>();
        tentacle2.Activate();
        tentacleIndex++;
        if (tentacleIndex == 3) tentacleIndex = 0;
        yield return new WaitForSeconds(2.5f);
        tentacleCoroutine = null;
    }
}
