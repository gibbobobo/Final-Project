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
    [SerializeField] float moveSpeed;
    [SerializeField] float xPadding;
    [SerializeField] float yPadding;
    [SerializeField] float firingRate;
    [SerializeField] GameObject missileProjectile;
    [SerializeField] GameObject deathExplosion;
    [SerializeField] bool invincible;
    [SerializeField] GameObject gameOverText;
    int playerLives;
    UIController uiController;
    SpriteRenderer spriteRenderer;
    bool isFiring = false;
    Coroutine firingCoroutine;
    LevelManager levelManager;

    private void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        uiController = GameObject.Find("Panel").GetComponent<UIController>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetBounds();
        invincible = false;
        playerLives = 3;
        spawnPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
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
    }

    void SetBounds()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Terrain")) && !invincible)
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
    }
      
    IEnumerator Firing()
    {
        while (true)
        {
            if (spriteRenderer.enabled == true)
            {
                Instantiate(missileProjectile, transform.position, Quaternion.identity);
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

}
