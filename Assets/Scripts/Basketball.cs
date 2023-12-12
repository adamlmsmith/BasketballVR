using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using TMPro;

public class Basketball : MonoBehaviour
{
    public event Action WasShot;

    [SerializeField]
    private float m_Timeout = 5.0f;

    private float m_TimeLeft = 0.0f;

    private bool m_WasShot = false;

    public bool WasCounted { get; set; } = false;

    public void Initialize()
    {
        m_WasShot = false;
        m_TimeLeft = m_Timeout;
        WasShot = null;
        WasCounted = false;
    }

    public void OnSelectExit(SelectExitEventArgs selectExitEventArgs)
    {
        // When the player lets go of the ball, count it as a shot
        WasShot?.Invoke();
        m_WasShot = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!WasCounted)
        {
            // When the ball collides with the floor, it's a missed shot
            if (other.gameObject.name == "Floor")
            {
                WasCounted = true;
                NotificationCenter.DefaultCenter().PostNotification(gameObject, "ShotMissed");
            }
            // When a ball collides with the hoop, it's a made shot
            else if (other.gameObject.name == "Hoop")
            {
                WasCounted = true;
                NotificationCenter.DefaultCenter().PostNotification(gameObject, "ShotMade");
            }
        }
    }

    void SelfDeactivate()
    {
        gameObject.SetActive(false);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().Sleep();
    }

    void Update()
    {
        // Disable the basketball after it times out
        if (m_WasShot)
        {
            m_TimeLeft -= Time.deltaTime;

            if (m_TimeLeft <= 0.0f)
            {
                SelfDeactivate();
            }
        }
    }
}
