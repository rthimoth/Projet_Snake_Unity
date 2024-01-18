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
    public int minEvent = 1;
    public int maxEvent = 10000;
    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onEvent = new UnityEvent();

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        SceneManager.LoadScene("Snake");
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
            currentScore += Time.deltaTime;
            GenerateEvent();
        }
    }
    private void GenerateEvent()
    {
        if (UnityEngine.Random.Range(minEvent, maxEvent) == 1)
        {
            onEvent.Invoke();
        }
    }


}