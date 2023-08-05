using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject deathExplosion;

    private void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();

        if (damage != null)
        {
            TakeDamage(damage.GetDamage());
            damage.Hit();
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
