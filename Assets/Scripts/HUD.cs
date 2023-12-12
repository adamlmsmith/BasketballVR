using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;

    [SerializeField]
    private TextMeshProUGUI m_StreakText;

    void Start()
    {
        Player.Instance.ScoreChanged += OnScoreChanged;
        Player.Instance.StreakChanged += OnStreakChanged;
    }

    void OnScoreChanged(int newScore)
    {
        m_ScoreText.text = "Score: " + newScore.ToString();
    }

    void OnStreakChanged(int newStreak)
    {
        m_StreakText.text = "Streak: " + newStreak.ToString();
    }
}
