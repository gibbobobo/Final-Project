using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaclecontroller : MonoBehaviour
{

    [SerializeField] int startingSprite;
    [SerializeField] PolygonCollider2D[] colliders;
    [SerializeField] int currentColliderIndex = 0;
    Animator anim;
    float offset;
    float frames;
    

    // Start is called before the first frame update
    void Start()
    {
        frames = colliders.Length;
        anim = GetComponent<Animator>();
        offset = startingSprite / frames;
        anim.Play(0, 0, offset);
    }

    public void SetColliderForSprite(int spriteIndex)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteIndex;
        colliders[currentColliderIndex].enabled = true;
    }

}
