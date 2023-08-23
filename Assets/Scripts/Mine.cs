using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    Vector3 offset = new Vector3(0, 1f, 0);
    Vector3 centre;
    [SerializeField] float radius; 
    

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
        PlayerControls player = collision.GetComponent<PlayerControls>();

        if (projectile != null)
        {
            projectile.Hit();
            DestroyMine();
        }
        else if (collision.gameObject.CompareTag("Player") && !player.IsInvincible())
        {
            player.PlayerHit();
            DestroyMine();
        }
    }

    void ExplosionDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(centre, radius);
        
        foreach (var hitcollider in colliders)
        {
            hitcollider.gameObject.SendMessage("TakeDamage", 100);    
        }
    }

    void DestroyMine()
    {
        Destroy(gameObject);
        centre = transform.position + offset;
        Instantiate(explosion, centre, Quaternion.identity);
        ExplosionDamage();
    }
}
