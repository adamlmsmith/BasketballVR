using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class DisplayFPS : MonoBehaviour
{
    [SerializeField]
    int m_UpdateDelay = 1000;

    private float m_TargetFPS = 72.0f;
    private float m_CurrentFPS = 0.0f;
    private float m_DeltaTime = 0.0f;
    private TextMeshProUGUI m_TextFPS;

    async void Start()
    {
        m_TextFPS = GetComponent<TextMeshProUGUI>();
        DisplayFramesPerSecond();
    }

    void Update()
    {
        GenerateFramesPerSecond();
    }

    private void GenerateFramesPerSecond()
    {
        m_DeltaTime += (Time.unscaledDeltaTime - m_DeltaTime) * 0.1f;
        m_CurrentFPS = 1.0f / m_DeltaTime;
    }

    private async Task DisplayFramesPerSecond()
    {
        while(true)
        {
            if (m_CurrentFPS >= m_TargetFPS)
            {
                m_TextFPS.color = Color.green;
            }
            else
            {
                m_TextFPS.color = Color.red;
            }

            m_TextFPS.text = "FPS: " + m_CurrentFPS.ToString(".0");
            await Task.Delay(500);
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class DisplayFPS : MonoBehaviour
// {
//     [SerializeField]
//     float m_UpdateDelay = 0.1f;

//     private float m_TargetFPS = 72.0f;
//     private float m_CurrentFPS = 0.0f;
//     private float m_DeltaTime = 0.0f;
//     private TextMeshProUGUI m_TextFPS;

//     void Start()
//     {
//         m_TextFPS = GetComponent<TextMeshProUGUI>();
//         StartCoroutine(DisplayFramesPerSecond());
//     }

//     void Update()
//     {
//         GenerateFramesPerSecond();
//     }

//     private void GenerateFramesPerSecond()
//     {
//         m_DeltaTime += (Time.unscaledDeltaTime - m_DeltaTime) * 0.1f;
//         m_CurrentFPS = 1.0f / m_DeltaTime;
//     }

//     private IEnumerator DisplayFramesPerSecond()
//     {
//         while(true)
//         {
//             if (m_CurrentFPS >= m_TargetFPS)
//             {
//                 m_TextFPS.color = Color.green;
//             }
//             else
//             {
//                 m_TextFPS.color = Color.red;
//             }

//             m_TextFPS.text = "FPS: " + m_CurrentFPS.ToString(".0");
//             yield return new WaitForSeconds(m_UpdateDelay);

//         }
//     }
// }
