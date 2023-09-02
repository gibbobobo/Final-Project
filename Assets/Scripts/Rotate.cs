using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] bool reverse;
    [SerializeField] float inside;
    [SerializeField] float outside;
    BossController bossController;

    private void Awake()
    {
        bossController = GameObject.Find("BossBody").GetComponent<BossController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossController.isActive)
        {
            transform.Rotate(0, 0, moveSpeed * Time.deltaTime);
            if (gameObject.transform.rotation.eulerAngles.z <= inside && !reverse)
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
}
