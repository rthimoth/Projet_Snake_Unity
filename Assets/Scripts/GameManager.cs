using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public float currentScore = 0f;
    public bool isPlaying = false;
    public int minEvent = 0;
    public int level = 1;
    public int maxEvent = 20000;
    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onReverseControl = new UnityEvent();
    public UnityEvent onExtraLife = new UnityEvent();

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        switch (level)
        {
            case 1:
                // Code for level 1
                Debug.Log("Starting level 1");
                SceneManager.LoadScene("Level1");
                break;
            case 2:
                // Code for level 2
                Debug.Log("Starting level 2");
                SceneManager.LoadScene("Level2"); // Replace "Level2" with your actual scene name for level 2
                break;
            case 3:
                // Code for level 3
                Debug.Log("Starting level 3");
                SceneManager.LoadScene("Level3"); // Replace "Level3" with your actual scene name for level 3
                break;
            default:
                Debug.Log("Invalid level");
                break;
        }
    }
    public void LoadOptionMenu()
    {
        SceneManager.LoadScene("Options");
    }
    public void SetLevels(string button_text)
    {
        string level_check;
        for (int i = 1; i < 4; i++)
        {
            level_check = i.ToString();
            if (button_text.Contains(level_check))
            {
                level = i;
            }
        }
    }
    public string FormatScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }
    public void GameOver()
    {
        currentScore = 0f;
        isPlaying = false;
        SceneManager.LoadScene("GameOver");
    }

    void Update()
    {
        if (isPlaying)
        {
            GenerateEvent();
        }
    }
    private void GenerateEvent()
    {
        if (UnityEngine.Random.Range(minEvent, maxEvent) == 1)
        {
            onReverseControl.Invoke();
        }
        if (UnityEngine.Random.Range(minEvent, maxEvent) == 2)
        {
            onExtraLife.Invoke();
        }
    }


}
