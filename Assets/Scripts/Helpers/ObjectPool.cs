using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> m_PooledObjects;
    private GameObject m_ObjectToPool;
    private int m_AmountToPool;

    public ObjectPool(GameObject objectToPool, int amountToPool)
    {
        m_PooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = GameObject.Instantiate(objectToPool);
            tmp.SetActive(false);
            m_PooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < m_PooledObjects.Count; i++)
        {
            if(!m_PooledObjects[i].activeInHierarchy)
            {
                return m_PooledObjects[i];
            }
        }
        
        return null;
    }
}
