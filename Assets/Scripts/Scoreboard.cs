using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] List<TMP_Text> highscoreNames;
    [SerializeField] List<TMP_Text> highscoreScores;
    [SerializeField] GameObject scoreName;
    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject enterName;
    [SerializeField] GameObject flashingText;
    TMP_InputField enterNameIF;
    static List<int> scores = new List<int>();
    static List<string> names = new List<string>();
    static bool loadedScores;
    TitleScreen titleScreen;

    private void Awake()
    {
        enterNameIF = enterName.GetComponent<TMP_InputField>();
        titleScreen = GameObject.Find("UI Canvas").GetComponent<TitleScreen>();
        if (!loadedScores) LoadScores();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (TitleScreen.gameOver)
        {
            titleScreen.PlayMusic();
            SetScores();
            flashingText.SetActive(false);
            scoreText.GetComponent<TextMeshProUGUI>().text = ScoreController.score.ToString();
            if (ScoreController.score > int.Parse(highscoreScores[5].text))
            {
                enterName.SetActive(true);
                enterNameIF.ActivateInputField();
                enterNameIF.onEndEdit.AddListener(HighScore);
                LoadScores();
            }
            else
            {
                scoreName.GetComponent<TextMeshProUGUI>().text = ("Your Score");
                Activate();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadScores()
    {
        scores.Clear();
        names.Clear();
        for(int i=0; i <= 5; i++)
        {
            scores.Add(int.Parse(highscoreScores[i].text));
            names.Add(highscoreNames[i].text);
        }
        loadedScores = true;
    }

    public void HighScore(string name)
    { 
        enterNameIF.onEndEdit.RemoveAllListeners();
        scoreName.GetComponent<TextMeshProUGUI>().text = name;
        enterName.SetActive(false);
        for (int i = 0; i <= 5; i++)
        {
             if (ScoreController.score > scores[i])
             {
                scores.Insert(i, ScoreController.score);
                names.Insert(i, name);
                break;
             }
        }
        SetScores();
        Activate();
    }

    void SetScores()
    {
        for (int i = 0; i <= 5; i++)
        {
            highscoreNames[i].text = names[i];
            highscoreScores[i].text = scores[i].ToString();
        }
    }

    void Activate()
    {
        flashingText.SetActive(true);
        titleScreen.Activate(1);
    }
}
