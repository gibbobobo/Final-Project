using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    [SerializeField] GameObject missileProjectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnFire()
    {
       Instantiate(missileProjectile, transform.position, Quaternion.identity);
    }
}
