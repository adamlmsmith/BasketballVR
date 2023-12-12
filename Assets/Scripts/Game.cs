using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    Timer m_Timer;

    void Start()
    {
        m_Timer.ResetTimer(30.0f);
        m_Timer.StartTimer();
    }
}
