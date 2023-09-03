using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] bool repeater;
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
        if (repeater && transform.position.x < -30)
        {
            transform.position = startPos;
        }
    }

    public void SetScroll(float speed)
    {
        moveSpeed = speed;
    }
}
