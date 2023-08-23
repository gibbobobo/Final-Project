using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaclecontroller : MonoBehaviour
{

    [SerializeField] int startingSprite;
    [SerializeField] PolygonCollider2D[] colliders;
    [SerializeField] int currentColliderIndex = 0;
    [SerializeField] Animator anim;
    float offset;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        offset = startingSprite / 30f;
        anim.Play("TentacleWhip", 0, offset);
    }

    public void SetColliderForSprite(int spriteIndex)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteIndex;
        colliders[currentColliderIndex].enabled = true;
    }

}
