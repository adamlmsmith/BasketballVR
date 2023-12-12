using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TimerText;

    private bool m_IsRunning = false;

    public float TimeLeft { get; private set; }

    private float MaxTime { get; set; }

    public void Awake()
    {
        TimeLeft = 0.0f;
    }

    public void ResetTimer(float maxTime)
    {
        MaxTime = maxTime;
        TimeLeft = maxTime;
        RefreshTimeText();
    }

    public void StartTimer()
    {
        m_IsRunning = true;
    }

    public void StopTimer()
    {
        m_IsRunning = false;
    }

    private void RefreshTimeText()
    {
        m_TimerText.text = "Time: " + Mathf.CeilToInt(TimeLeft).ToString();
    }

    private void Update()
    {
        if (m_IsRunning && TimeLeft > 0.0f)
        {
            TimeLeft -= Time.deltaTime;

            RefreshTimeText();

            if (TimeLeft <= 0.0f)
            {
                TimeLeft = 0.0f;
            }
        }
    }
}
