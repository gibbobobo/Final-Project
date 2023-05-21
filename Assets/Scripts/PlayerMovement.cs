using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 rawInput;
    Vector2 minBound;
    Vector2 maxBound;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float padding = 1f;


    // Start is called before the first frame update
    void Start()
    {
        SetBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + movement.x, minBound.x + padding, maxBound.x - padding);
        newPos.y = Mathf.Clamp(transform.position.y + movement.y, minBound.y + padding, maxBound.y - padding);
        transform.position = newPos;
    }

    void SetBounds()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }
}
