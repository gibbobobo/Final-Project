using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{ 
    [SerializeField] float driftSpeed;
    public int type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0)
        {
            Vector3 movement = new Vector2(0, -driftSpeed * Time.deltaTime);
            transform.position -= movement;
        }
    }

    public void SetType(int _type)
    {
        type = _type;
    }


}
