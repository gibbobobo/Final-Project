using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject lives1;
    [SerializeField] GameObject lives2;
    [SerializeField] GameObject lives3;
    [SerializeField] GameObject chain1;
    [SerializeField] GameObject chain2;
    [SerializeField] GameObject chain3;
    [SerializeField] GameObject multiplerDisplay;

    int currentType;
    int streak;
    int multiplier;

    // Start is called before the first frame update
    void Start()
    {
        lives1.GetComponent<Image>().enabled = true;
        lives2.GetComponent<Image>().enabled = true;
        lives3.GetComponent<Image>().enabled = false;
        chain1.GetComponent<Image>().enabled = false;
        chain2.GetComponent<Image>().enabled = false;
        chain3.GetComponent<Image>().enabled = false;
        streak = 0;
        multiplier = 1;
        ScoreController.score = 0;
    }

    public void UpdateLives(int playerLives)
    {
        if (playerLives >= 2)
        {
            lives1.GetComponent<Image>().enabled = true;
        }
        else
        {
            lives1.GetComponent<Image>().enabled = false;
        }
        if (playerLives >= 3)
        {
            lives2.GetComponent<Image>().enabled = true;
        }
        else
        {
            lives2.GetComponent<Image>().enabled = false;
        }
        if (playerLives >= 4)
        {
            lives3.GetComponent<Image>().enabled = true;
        }
        else
        {
            lives3.GetComponent<Image>().enabled = false;
        }

    }

    private void UpdateStreakDisplay()
    {
        if (streak >= 1)
        {
            chain1.GetComponent<Image>().enabled = true;
        }
        else
        {
            chain1.GetComponent<Image>().enabled = false;
        }
        if (streak >= 2)
        {
            chain2.GetComponent<Image>().enabled = true;
        }
        else
        {
            chain2.GetComponent<Image>().enabled = false;
        }
        if (streak >= 3)
        {
            chain3.GetComponent<Image>().enabled = true;
        }
        else
        {
            chain3.GetComponent<Image>().enabled = false;
        }
    }

    public void UpdateScore(int type, int points)
    {
        if (type != currentType)
        {
            streak = 1;
            currentType = type;
        }
        else
        {
            streak += 1;
        }
        if (streak == 3)
        {
            multiplier += 1;
            multiplerDisplay.GetComponent<TextMeshProUGUI>().text = "x" + multiplier.ToString();
        }
        if (streak > 3) streak = 1;
        UpdateStreakDisplay();
        ScoreController.score += (points * multiplier);
    }

    public void ResetStreak()
    {
        streak = 0;
        UpdateStreakDisplay();
    }
}
