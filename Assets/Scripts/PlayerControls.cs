using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControls : MonoBehaviour
{

    Vector2 rawInput;
    Vector2 minBound;
    Vector2 maxBound;
    Vector3 spawnPos;
    float xPadding;
    float yPadding;
    [SerializeField] float moveSpeed;
    [SerializeField] float firingRate;
    [SerializeField] GameObject missileProjectile;
    [SerializeField] GameObject deathExplosion;
    [SerializeField] bool invincible;
    GameObject gameOverText;
    GameObject spawnList;
    int playerLives;
    UIController uiController;
    SpriteRenderer spriteRenderer;
    bool isFiring;
    Coroutine firingCoroutine;
    LevelManager levelManager;
    List<GameObject> spawnPoints = new List<GameObject>();
    int spawnIndex;

    private void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        uiController = GameObject.Find("UI Panel").GetComponent<UIController>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        gameOverText = GameObject.Find("Game Over (TMP)");
        spawnList = GameObject.Find("SpawnPoints");
    }

    // Start is called before the first frame update
    void Start()
    {
        SetBounds();
        invincible = false;
        isFiring = false;
        playerLives = 3;
        GetSpawnPoints();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (isFiring && firingCoroutine == null && spriteRenderer.enabled == true)
        {
            firingCoroutine = StartCoroutine(Firing());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
        Vector3 nextSpawn = Camera.main.WorldToScreenPoint(spawnList.transform.GetChild(spawnIndex + 1).position);
        if (nextSpawn.x < spawnPos.x)
        {
            spawnPos = Camera.main.WorldToScreenPoint(spawnList.transform.GetChild(spawnIndex + 1).position);
            spawnIndex += 1;
        }
    }

    void GetSpawnPoints()
    {
        foreach (Transform child in spawnList.transform)
        {
            spawnPoints.Add(child.gameObject);
        }
        spawnIndex = 0;
        spawnPos = Camera.main.WorldToScreenPoint(spawnList.transform.GetChild(0).position);
    }

    void SetBounds()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        xPadding = mainCamera.orthographicSize * 2f * 0.06f;
        yPadding = mainCamera.orthographicSize * mainCamera.aspect * 0.06f;
    }

    void Move()
    {
        Vector2 movement = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + movement.x, minBound.x + xPadding, maxBound.x - xPadding);
        newPos.y = Mathf.Clamp(transform.position.y + movement.y, minBound.y + yPadding, maxBound.y - yPadding);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }

    void TakeDamage(int damage)
    {
        if (damage > 0 && !invincible)
        {
            PlayerHit();
        }
    }

    public void PlayerHit()
    {
        playerLives -= 1;
        uiController.UpdateLives(playerLives);
        invincible = true;
        spriteRenderer.enabled = false;
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
        if (playerLives > 0)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Terrain")) && !invincible)
        {
            PlayerHit();
        }
        else if (collision.gameObject.CompareTag("Terrain") && invincible)
        {
            Vector3 respawnPos = Camera.main.ScreenToWorldPoint(spawnPos);
            transform.position = respawnPos;
        }
    }
      
    IEnumerator Firing()
    {
        while (true)
        {
            if (spriteRenderer.enabled == true)
            {
                Instantiate(missileProjectile, transform.position + new Vector3(0,-0.15f,0), Quaternion.identity);
            }
            yield return new WaitForSeconds(firingRate);
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        uiController.ResetStreak();
        Vector3 respawnPos = Camera.main.ScreenToWorldPoint(spawnPos);
        transform.position = respawnPos;
        spriteRenderer.enabled = true;
        Color oldColor = spriteRenderer.color;
        Color flashColor = new Color(255, 255, 255, 0.5f);
        for (var i = 0; i < 10; i++)
        {
            spriteRenderer.material.color = flashColor;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.material.color = oldColor;
            yield return new WaitForSeconds(0.05f);
        }
        invincible = false;
    }

    IEnumerator GameOver()
    {
        gameOverText.GetComponent<TextMeshProUGUI>().enabled = true;
        yield return new WaitForSeconds(3.0f);
        levelManager.LoadGameOver();
    }

    public bool IsInvincible()
    {
        return invincible;
    }
}
