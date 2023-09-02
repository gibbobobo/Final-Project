using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaclecontroller : MonoBehaviour
{

    [SerializeField] int startingSprite;
    [SerializeField] PolygonCollider2D[] colliders;
    [SerializeField] int currentColliderIndex = 0;
    [SerializeField] float movespeed;
    Animator anim;
    float offset;
    float frames;
    [SerializeField] bool active;
    bool moved;
    [SerializeField] Vector3 moveDist;
    [SerializeField] bool top;
    Vector3 startPosition;
    Vector3 targetPositon;

    // Start is called before the first frame update
    void Start()
    {
        frames = colliders.Length;
        anim = GetComponent<Animator>();
        offset = startingSprite / frames;
        anim.Play(0, 0, offset);
        moved = false;
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPositon, movespeed * Time.deltaTime);
            if (transform.position == targetPositon && !moved)
            {
                targetPositon = startPosition;
                moved = true;
            }
            if (transform.position == startPosition && moved)
            {
                active = false;
            }
        }
    }

    public void SetColliderForSprite(int spriteIndex)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteIndex;
        colliders[currentColliderIndex].enabled = true;
    }

    public void Activate()
    {
        active = true;
        startPosition = transform.position;
        targetPositon = transform.position + moveDist;
        moved = false;
    }

}
