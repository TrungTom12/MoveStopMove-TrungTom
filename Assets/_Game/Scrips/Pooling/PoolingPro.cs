using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolingPro : Singleton<PoolingPro>
{
    [System.Serializable]
    public class Pool
    {
        public GameObject poolObjectPrefab;
        public int poolCount;
        public bool canGrow;
        public string tag;
    }

    public List<Pool> poolList = new List<Pool>();
    Bullet bullet;
    Dictionary<string, List<GameObject>> objectPools = new Dictionary<string, List<GameObject>>();
    Dictionary<string, List<GameObject>> activeObjectPools = new Dictionary<string, List<GameObject>>();

    void Start()
    {
        GetInstance();
        foreach (Pool pool in poolList)
        {
            List<GameObject> l = new List<GameObject>();
            for (int i = 0; i < pool.poolCount; i++)
            {
                GameObject obj = Instantiate(pool.poolObjectPrefab);
                obj.SetActive(false);
                l.Add(obj);
            }

            objectPools[pool.tag] = l;
            activeObjectPools[pool.tag] = new List<GameObject>();

        }
        GameManager.GetInstance().L_character = SpawnManager.GetInstance().SpawnBot(GameManager.GetInstance().numBot);
    }

    public GameObject GetFromPool(string tag)
    {
        Pool tempPool = new Pool();
        foreach (Pool pool in poolList)
        {
            if (tag == pool.tag)
            {
                tempPool = pool;
                break;
            }
        }

        if (objectPools[tag].Count > 0)
        {
            GameObject go = objectPools[tag][0];
            objectPools[tag].RemoveAt(0);
            return go;
        }

        else if (tempPool.canGrow)
        {
            GameObject go = objectPools[tag][0];
            objectPools[tag].RemoveAt(0);
            return go;

        }
        else if (tempPool.canGrow)
        {
            GameObject obj = Instantiate(tempPool.poolObjectPrefab);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public GameObject GetFromPool(string tag, Vector3 pos)
    {
        Pool tempPool = new Pool();

        foreach (Pool pool in poolList)
        {
            if (tag == pool.tag)
            {
                tempPool = pool;
                break;
            }
        }

        if (objectPools[tag].Count > 0)
        {
            GameObject go = objectPools[tag][0];
            go.transform.position = pos;
            go.SetActive(true);
            objectPools[tag].RemoveAt(0);

            return go;
        }
        else if (tempPool.canGrow)
        {
            GameObject go = Instantiate(tempPool.poolObjectPrefab);
            go.transform.position = pos;
            go.SetActive(true);
            objectPools[tag].Remove(go);
            return go;
        }
        else
        {
            return null;
        }

    }

    public void ReturnToPool(string tag, GameObject go)
    {
        Pool tempPool = new Pool();
        foreach (Pool pool in poolList)
        {
            if (tag == pool.tag)
            {
                tempPool = pool;
                break;
            }
        }
        switch (tag)
        {
            case "Stick":
                go.transform.rotation = tempPool.poolObjectPrefab.transform.rotation;
                go.GetComponent<Bullet>().ResetForce();
                go.GetComponent<Bullet>().Timer = 0;
                activeObjectPools[tag].Remove(go);
                objectPools[tag].Add(go);
                go.SetActive(false);
                break;
        }
    }

}