using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollChanger : MonoBehaviour
{
    [SerializeField] float newSpeed;
    Scroll scroll;
    BackgroundScroll bgScroll1;
    BackgroundScroll bgScroll2;
    BackgroundScroll bgScroll3;
    BossController boss;
    Vector3 screenPos;
    bool passed;

    private void Awake()
    {
        scroll = GameObject.Find("Scrolling Level").GetComponent<Scroll>();
        bgScroll1 = GameObject.Find("Background (0)").GetComponent<BackgroundScroll>();
        bgScroll2 = GameObject.Find("Background (1)").GetComponent<BackgroundScroll>();
        bgScroll3 = GameObject.Find("Background (2)").GetComponent<BackgroundScroll>();
        boss = GameObject.Find("BossBody").GetComponent<BossController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        passed = false;
    }

    // Update is called once per frame
    void Update()
    {
        screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        if (!passed && screenPos.x <= 0)
        {
            scroll.SetScroll(newSpeed);
            passed = true;
            if (newSpeed == 0)
            {
                bgScroll1.StopScroll();
                bgScroll2.StopScroll();
                bgScroll3.StopScroll();
                boss.Activate();
            }
        }
    }
}
