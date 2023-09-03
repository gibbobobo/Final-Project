using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    public bool isActive;
    LevelManager levelManager;
    Coroutine cycle;
    [SerializeField] List<GameObject> titleScreens;
    [SerializeField] GameObject htpScreen;
    [SerializeField] List<GameObject> fishes;
    [SerializeField] GameObject scoreName;
    [SerializeField] GameObject scoreText;
    AudioSource music;

    int index;
    public static bool gameOver;

    private void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        music = Camera.main.GetComponent<AudioSource>();
        if (gameOver)
        {
            GetComponent<Animator>().enabled = false;
            titleScreens[1].SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(int value)
    {
        index = value;
        isActive = true;
        GetComponent<Animator>().enabled = false;
        PlayMusic();
        cycle = StartCoroutine(CycleScreens());
    }  

    public void PlayMusic()
    {
        if (!music.isPlaying) music.Play();
    }

    public void OnInsertCoin(InputValue value)
    {
        if (isActive)
        {
            StopCoroutine(cycle);
            foreach (GameObject child in titleScreens)
            {
                child.SetActive(false);
            }
            foreach (GameObject child in fishes)
            {
                child.SetActive(false);
            }
            htpScreen.SetActive(true);
        }
    }

    public void OnFire()
    {
        if (htpScreen.activeInHierarchy)
        {
            levelManager.LoadGame();
        }
    }

    IEnumerator CycleScreens()
    {
        while (true)
        {
            yield return new WaitForSeconds(8);
            scoreName.GetComponent<TextMeshProUGUI>().text = ("");
            scoreText.GetComponent<TextMeshProUGUI>().text = ("");
            index++;
            if (index == 3) index = 0;
            for (var i = 0; i < titleScreens.Count; i++)
            {
                if (i != index)
                {
                    
                    titleScreens[i].SetActive(false);
                }
                else titleScreens[i].SetActive(true);
            }
            
        }
    }
}
