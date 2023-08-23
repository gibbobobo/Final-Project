using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] bool reverse;
    [SerializeField] float inside;
    [SerializeField] float outside;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, moveSpeed * Time.deltaTime);
        Debug.Log(gameObject.transform.rotation.eulerAngles.z);
        if (gameObject.transform.rotation.eulerAngles.z <= inside  && !reverse)
        {
            moveSpeed = -moveSpeed;
            reverse = !reverse;
        }
        if (gameObject.transform.rotation.eulerAngles.z >= outside && reverse)
        {
            moveSpeed = -moveSpeed;
            reverse = !reverse;
        }
    }
}
