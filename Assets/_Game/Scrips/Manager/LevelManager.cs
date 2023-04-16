using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private int levelId = 0;
    [SerializeField] private Level currentLevel;
    [SerializeField] List<GameObject> allLevelPrefabs;

    public Level CurrentLevel { get => currentLevel; set => currentLevel = value; }

    private void Start()
    {

    }
    void Goto(int level)
    {
        SpawnManager.GetInstance().SpawnBot(10);
    }
    public void LoadLevel()
    {
        currentLevel = Instantiate(allLevelPrefabs[levelId]).GetComponent<Level>();
        GameManager.GetInstance().OnInit(currentLevel);
    }
    public void NextLevel()
    {
        Destroy(currentLevel.gameObject);
        levelId++;
        LoadLevel();
    }
}
