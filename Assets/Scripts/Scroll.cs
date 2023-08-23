using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector2(moveSpeed * Time.deltaTime, 0);
        transform.position -= movement;
    }

    public void SetScroll(float speed)
    {
        moveSpeed = speed;
    }
}
