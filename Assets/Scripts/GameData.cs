using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
using TMPro;


public class GameData : MonoBehaviour
{
    public List<Level> levels;
    public float pauseTime = 2f;
    public TextMeshProUGUI coinCounter;
    public RectTransform coinLogo;
    public Transform coinLogo3D;
    public Transform bar3D;
    public SliderController sc;
    public Text pause, victory;
    public GameObject retryScreen, victoryScreen, startScreen, tutorialScreen;
    public TextMeshProUGUI completionPercentage, levelCompleted, bossText;
    public CinemachineVirtualCamera playerVirtualCamera;
    public bool leftButtonisPressed { get; set; }
    public bool rightButtonisPressed { get; set; }
    public bool resetButtonisPressed { get; set; }
    public float timePassedSincePress;
    public float tutorialScreenDelay = 1f;

    static GameData instance;
    GameObject exit;
    float resetButtonHoldTime;
    bool tutorialScreenWasShown = false;

    private void Awake()
    {
        //let's hope there are no more instances of this, but just in case to be safe...
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }   
    }

    private void Start()
    {
        int levelToLoad = PlayerPrefs.GetInt("Level Index", 1);
        //if we want to load more levels than we have, just load a random one
        if (levelToLoad >= levels.Count)
        {
            levelToLoad = UnityEngine.Random.Range(1, levels.Count);
            PlayerPrefs.SetInt("Level Index", levelToLoad);
        }
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadScene(levelToLoad);
        
        Invoke("PauseGame", CinemachineCore.Instance.FindPotentialTargetBrain(playerVirtualCamera).m_DefaultBlend.BlendTime);
    }

    void Update()
    {
        if(resetButtonisPressed)
        {
            resetButtonHoldTime += Time.deltaTime;
            if(resetButtonHoldTime > 10)
            {
                resetButtonHoldTime = 0;
                //unity loses track of this after scene reload
                resetButtonisPressed = false;
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            resetButtonHoldTime = 0;
        }
    }

    public void PauseGame()
    {
        startScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        startScreen.SetActive(false);
        Time.timeScale = 1;
        //if we are playing the first time and not retrying
        if(PlayerPrefs.GetString("Level", "1") == "1" && !tutorialScreenWasShown)
        {
            StartCoroutine(ShowTutorial());
        }    
    }

    IEnumerator ShowTutorial()
    {
        tutorialScreenWasShown = true;
        yield return new WaitForSeconds(tutorialScreenDelay);
        Time.timeScale = 0;
        tutorialScreen.SetActive(true);
    }

    public void HideTutorial()
    {
        Time.timeScale = 1;
        tutorialScreen.SetActive(false);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode lsm)
    {
        coinCounter.text = PlayerPrefs.GetString("Score", "0");
        sc.currentLvlTxt.text = PlayerPrefs.GetString("Level", "1");
        sc.nextLvlTxt.text = (System.Int32.Parse(sc.currentLvlTxt.text) + 1).ToString();
        SceneManager.SetActiveScene(scene);
        exit = GameObject.FindGameObjectWithTag("Exit");
        if (exit)
        {
            exit.GetComponent<Exit>().gd = this;
            exit.SetActive(false);
        }
    }
    
    public void LoadNewLevel()
    {
        PlayerPrefs.SetString("Level", sc.nextLvlTxt.text);
        PlayerPrefs.SetString("Score", coinCounter.text);
        //if we ran out of levels, set a random one
        if (System.Int32.Parse(PlayerPrefs.GetString("Level")) >= levels.Count)
        {
            int levelToLoad = UnityEngine.Random.Range(1, levels.Count);
            PlayerPrefs.SetInt("Level Index", levelToLoad);
        }
        //else simply set next one
        else
        {
            PlayerPrefs.SetInt("Level Index", SceneManager.GetActiveScene().buildIndex + 1);
        }
        levelCompleted.text = "{offset}LEVEL COMPLETE!{/offset}";
        victoryScreen.SetActive(true);
    }

    public void ReloadScene()
    {
        retryScreen.SetActive(false);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level Index", 1));
    }

    public void LoadNextScene()
    {
        victoryScreen.SetActive(false);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level Index"));
    }

    public void AllShardsCollected()
    {
        exit.SetActive(true);
        exit.transform.parent.GetComponent<Animator>().SetTrigger("open");
    }
}

[Serializable]
public struct Level
{
    public int shardsToPass;
}