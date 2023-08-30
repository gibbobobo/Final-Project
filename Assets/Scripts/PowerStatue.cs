using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStatue : MonoBehaviour
{
    [SerializeField] int type;
    [SerializeField] int health;
    [SerializeField] List<GameObject> powerUp;
    [SerializeField] GameObject explosion;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (health == 0)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity, transform.parent);
            GameObject powerOrb;
            powerOrb = Instantiate(powerUp[type], transform.position, Quaternion.identity, transform.parent);
            PowerUp PU = powerOrb.GetComponent<PowerUp>();
            PU.SetType(type);
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
