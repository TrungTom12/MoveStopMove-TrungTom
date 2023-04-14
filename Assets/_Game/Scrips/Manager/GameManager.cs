using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int Alive;
    public int numBot = 10;
    public CameraFollow cameraFollow;
    [SerializeField] private List <Transform> l_character = new List<Transform> ();
    public List<Transform> L_character { get => l_character; set => l_character = value; }

    [SerializeField] private List<Transform> l_SpawnBot = new List<Transform>();
    public List<Transform> L_SpawnBot { get => l_SpawnBot; set => l_SpawnBot = value; }

    private int numSpawn;
    public int NumSpawn { get => numSpawn; set => numSpawn = value; }
    

    private void Start()
    {
        GetInstance();
        UIManager.GetInstance().SetAliveText(Alive);
        numSpawn = Alive - numBot;
    }

    public bool IsSpawnEnemy()
    {
        return numSpawn > 0;
    }

    public void UpdateAliveText()
    {
        Alive -= 1;
        UIManager.GetInstance().SetAliveText(Alive);
    } 

    public Vector3 GetRandomSpawnPos()
    {
        List<Vector3> l_Spawn = new List<Vector3> ();
        foreach(Transform tran in l_SpawnBot)
        {
            if (!tran.GetComponent<SpawnPos>().IsAnyPlayer())
            {
                l_Spawn.Add(tran.position);
            }
        }

        return l_Spawn[Random.Range(0,l_Spawn.Count)];
    }

}
