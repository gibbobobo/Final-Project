using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] float offset;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector2(moveSpeed * Time.deltaTime, 0);
        transform.position -= movement;
        if(transform.position.x < startPos.x - offset)
        {
            transform.position = startPos;
        }
        
    }

    public void StopScroll()
    {
        moveSpeed = 0;
    }
}
