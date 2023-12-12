using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePoint : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_ScoreText;

    void OnSelfDestroy()
    {
        gameObject.SetActive(false);
    }

    public void SetPointText(int points)
    {
        m_ScoreText.text = "+" + points.ToString();
    }
}
