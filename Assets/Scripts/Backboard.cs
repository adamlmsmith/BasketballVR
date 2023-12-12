using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backboard : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Hoop;

    [SerializeField]
    ScorePoint m_ScorePointPrefab;

    [SerializeField]
    private Vector3 m_MoveDistance = new Vector3(3.0f, 0.0f, 0.0f);

    private Vector3 m_StartPosition;
    private Vector3 m_GoalPosition;

    private float m_MoveStartTime;
    private float m_MoveDuration = 5.0f;

    private ObjectPool m_ScorePointPool;

    enum MoveDirection { LEFT, RIGHT };

    MoveDirection m_MoveDirection = MoveDirection.RIGHT;

    void Awake()
    {
        // Create an object pool of points that float up from the hoop
        m_ScorePointPool = new ObjectPool(m_ScorePointPrefab.gameObject, 5);

        // Initialize the movement variables
        m_StartPosition = transform.position - (m_MoveDistance * 0.5f);
        SetGoalPosition(transform.position + (m_MoveDistance * 0.5f));

        Player.Instance.PointsScored += PointsScored;
    }

    void Update()
    {
        // Move the backboard left and right
        if (transform.position != m_GoalPosition)
        {
            float t = (Time.time - m_MoveStartTime) / m_MoveDuration;
            transform.position = new Vector3(Mathf.SmoothStep(m_StartPosition.x, m_GoalPosition.x, t), Mathf.SmoothStep(m_StartPosition.y, m_GoalPosition.y, t), Mathf.SmoothStep(m_StartPosition.z, m_GoalPosition.z, t));

            if (transform.position == m_GoalPosition)
            {
                switch (m_MoveDirection)
                {
                    case MoveDirection.LEFT:
                        m_MoveDirection = MoveDirection.RIGHT;
                        SetGoalPosition(transform.position + m_MoveDistance);
                        break;
                    case MoveDirection.RIGHT:
                        m_MoveDirection = MoveDirection.LEFT; 
                        SetGoalPosition(transform.position - m_MoveDistance);
                        break;
                }
            }
        }
    }

    public void SetGoalPosition(Vector3 goalPosition)
    {
        m_GoalPosition = goalPosition;
        m_StartPosition = transform.position;
        m_MoveStartTime = Time.time;
    }

    public void PointsScored(int numPoints)
    {
        // When points are scored, make a number float up from the hoop
        ScorePoint scorePoint = m_ScorePointPool.GetPooledObject().GetComponent<ScorePoint>();
        scorePoint.gameObject.SetActive(true);
        scorePoint.transform.position = m_Hoop.transform.position;
        scorePoint.SetPointText(numPoints);
        scorePoint.GetComponent<Animator>().Play("FloatUp");
    }
}
