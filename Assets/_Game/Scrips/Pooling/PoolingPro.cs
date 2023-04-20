using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum WeaponHold
{
    AxeHold, KnifeHold, BoomerangHold, CandyHold
};

public enum WeaponShow
{
    AxeShow, KnifeShow, BoomerangShow, CandyShow,
}

public enum Pant
{
    Pant1, Pant2
}

public enum Head
{
    Head1, Head2
}

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

    
    Bullet bullet;
    public List<Pool> poolList = new List<Pool>();
    
    Dictionary<string, List<GameObject>> objectPools = new Dictionary<string, List<GameObject>>(); //GameObject
    Dictionary<string, List<GameObject>> activeObjectPools = new Dictionary<string, List<GameObject>>(); // GameObject dc kich hoạt 

    public List<WeaponType> weapons;
    public Dictionary<WeaponType, WeaponHold> weaponHolds = new Dictionary<WeaponType, WeaponHold>();
    public Dictionary<WeaponType, WeaponShow> weaponShows = new Dictionary<WeaponType, WeaponShow>();

    public List<Material> pantMaterials;
    public Dictionary<Pant,Material> pantMaterial = new Dictionary<Pant, Material>();
	
    void Start()
    {
        GetInstance();
        weapons.Add(WeaponType.Axe);
        weapons.Add(WeaponType.Candy);
        weapons.Add(WeaponType.Knife);
        weapons.Add(WeaponType.Boomerang);

        weaponHolds[WeaponType.Axe] = WeaponHold.AxeHold;
        weaponHolds[WeaponType.Knife] = WeaponHold.KnifeHold;
        weaponHolds[WeaponType.Boomerang] = WeaponHold.BoomerangHold;
        weaponHolds[WeaponType.Candy] = WeaponHold.CandyHold;
		
		weaponShows[WeaponType.Axe] = WeaponShow.AxeShow;
        weaponShows[WeaponType.Knife] = WeaponShow.KnifeShow;
        weaponShows[WeaponType.Boomerang] = WeaponShow.BoomerangShow;
        weaponShows[WeaponType.Candy] = WeaponShow.CandyShow;

        pantMaterial[Pant.Pant1] = pantMaterials[0];
        pantMaterial[Pant.Pant2] = pantMaterials[1];

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
		NewUIManager.GetInstance().OpenUI<MainMenu>();
        
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
            activeObjectPools[tag].Add(go);
            switch (tag)
            {
                case "Boomerang":
                    go.GetComponent<Boomerang>().SetFirstPoint(pos);
                    break;

            }
            return go;
        }

        else if (tempPool.canGrow)
        {
            GameObject go = Instantiate(tempPool.poolObjectPrefab);
            go.transform.position = pos;
            go.SetActive(true);
            objectPools[tag].Remove(go);
            activeObjectPools[tag].Add(go);
            switch (tag)
            {
                case "Boomerang":
                    go.GetComponent<Boomerang>().SetFirstPoint(pos);
                    break;

            }
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
            case "Bot":
                go.GetComponent<Bot>().OnInit();
                BasicReset(tag, go, tempPool);
                break;

            case "Player":
                BasicReset(tag, go, tempPool);
                go.GetComponent<Player>().OnInit();
                break;

            case "Stick":
            case "Candy":
            case "Boomerang":
            case "Uzi":
            case "Knife":
            case "Axe":
                WeaponReset(tag, go, tempPool);
                break;

            case "AxeShow":
            case "BoomerangShow":
            case "KnifeShow":
            case "CandyShow":
                activeObjectPools[tag].Remove(go);
                objectPools[tag].Add(go);
                go.SetActive(false);
                break;

            case "AxeHold":
            case "BoomerangHold":
            case "KnifeHold":
            case "CandyHold":
                BasicReset(tag, go, tempPool);
                break;

            default:
                BasicReset(tag, go, tempPool);
                break;

        }
    }

    public void BasicReset(string tag, GameObject go, Pool tempPool)
    {
        go.transform.rotation = tempPool.poolObjectPrefab.transform.rotation;
        go.transform.localScale = tempPool.poolObjectPrefab.transform.localScale;
        activeObjectPools[tag].Remove(go);
        objectPools[tag].Add(go);
        go.SetActive(false);
    }
    public void WeaponReset(string tag, GameObject go, Pool tempPool)
    {
        go.GetComponent<Bullet>().ResetForce();
        go.GetComponent<Bullet>().Timer = 0;
        BasicReset(tag, go, tempPool);
    }
    public void ClearObjectActive(string tag)
    {
        while (activeObjectPools[tag].Count > 0)
        {
            ReturnToPool(tag, activeObjectPools[tag][0]);
        }
    }
}
