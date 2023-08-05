using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointData : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float moveDelay;
    [SerializeField] float fireDelay;
    [SerializeField] bool attacking;

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetMoveDelay()
    {
        return moveDelay;
    }

    public float GetFireDelay()
    {
        return fireDelay;
    }
    public bool IsAttacking()
    {
        return attacking;
    }

    public Transform GetRotation()
    {
        return gameObject.transform;
    }
}
