using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel;
    void Goto(int level)
    {
        SpawnManager.GetInstance().SpawnBot(10);
    }
}
