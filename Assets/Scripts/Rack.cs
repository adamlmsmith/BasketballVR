using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rack : MonoBehaviour
{
    [SerializeField]
    private Transform m_BallSpawnPosition;

    [SerializeField]
    private GameObject m_BasketballPrefab;

    private ObjectPool m_BasketballPool;

    void Start()
    {
        m_BasketballPool = new ObjectPool(m_BasketballPrefab, 20);
        SpawnBasketball();
    }

    void SpawnBasketball()
    {
        Basketball basketball = m_BasketballPool.GetPooledObject().GetComponent<Basketball>();
        if (basketball != null)
        {
            basketball.Initialize();
            basketball.transform.position = m_BallSpawnPosition.transform.position;
            basketball.transform.rotation = m_BallSpawnPosition.transform.rotation;
            basketball.gameObject.SetActive(true);

            basketball.WasShot += BasketballShot;
        }
    }

    public void BasketballShot()
    {
        // After each shot, spawn a new basketball
        SpawnBasketball();
    }
}
