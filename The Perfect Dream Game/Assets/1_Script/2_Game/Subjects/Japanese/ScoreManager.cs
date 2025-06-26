using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public float timeLimit = 60f;
    private float timer;

    public GameObject resultPanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = timeLimit;
        UpdateScoreText();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Time.timeScale = 0f;
            resultPanel.SetActive(true);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}