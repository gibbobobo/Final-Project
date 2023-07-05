using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    Vector2 rawInput;
    Vector2 minBound;
    Vector2 maxBound;
    [SerializeField] float moveSpeed;
    [SerializeField] float xPadding;
    [SerializeField] float yPadding;
    [SerializeField] GameObject missileProjectile;
    [SerializeField] GameObject deathExplosion;


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
        newPos.x = Mathf.Clamp(transform.position.x + movement.x, minBound.x + xPadding, maxBound.x - xPadding);
        newPos.y = Mathf.Clamp(transform.position.y + movement.y, minBound.y + yPadding, maxBound.y - yPadding);
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

    void OnFire()
    {
        Instantiate(missileProjectile, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
        }
    }
        

}
