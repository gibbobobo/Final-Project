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
        Damage damage = collision.GetComponent<Damage>();

        if (damage != null)
        {
            Destroy(gameObject);
            centre = transform.position + offset;
            Instantiate(explosion, centre, Quaternion.identity);
            damage.Hit();
            ExplosionDamage();
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
}
