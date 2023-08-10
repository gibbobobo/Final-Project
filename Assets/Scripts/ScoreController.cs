using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject scoreText;

    public static int score;

    void Update()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
