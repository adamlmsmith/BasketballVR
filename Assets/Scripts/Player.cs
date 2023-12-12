using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Singleton<Player>
{
    public event Action<int> ScoreChanged;
    public event Action<int> StreakChanged;
    public event Action<int> PointsScored;

    const int k_PointsPerBasketNormal = 1;
    const int k_PointsPerBasketStreak = 3;
    const int k_ConsecutiveBasketsForStreak = 3;

    int m_Score = 0;
    int m_Streak = 0;

    private int Score { get { return m_Score; } set { m_Score = value; ScoreChanged?.Invoke(m_Score); } }
    public int Streak { get { return m_Streak; } private set { m_Streak = value; StreakChanged?.Invoke(m_Streak); } }

    void Awake()
    {
        base.Awake();
        NotificationCenter.DefaultCenter().AddObserver(gameObject, "ShotMissed");
        NotificationCenter.DefaultCenter().AddObserver(gameObject, "ShotMade");
    }

    void Start()
    {
        Streak = 0;
    }

    public void ShotMade()
    {
        int newPoints;

        Streak++;
        // If the player is on a streak, it counts for more points
        newPoints = (Streak >= k_ConsecutiveBasketsForStreak ? k_PointsPerBasketStreak : k_PointsPerBasketNormal);

        Score += newPoints;
        PointsScored?.Invoke(newPoints);
    }

    public void ShotMissed()
    {
        Streak = 0;
    }
}
