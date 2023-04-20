using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private int levelId = 0;
    [SerializeField] private Level currentLevel;
    [SerializeField] List<GameObject> allLevelPrefabs;

    public Level CurrentLevel { get => currentLevel; set => currentLevel = value; }

    
    void Goto(int level)
    {
        SpawnManager.GetInstance().SpawnBot(10);
    }
    public void LoadLevel()
    {
        GameManager.GetInstance().cameraFollow.ResetOffset();
        GameManager.GetInstance().ClearObjectSpawn();
        currentLevel = Instantiate(allLevelPrefabs[levelId]).GetComponent<Level>();
        GameManager.GetInstance().OnInit(currentLevel);
    }
    public void NextLevel()
    {
        Destroy(currentLevel.gameObject);
        levelId++;
        LoadLevel();
    }

    public void Replay()
    {
        Destroy(currentLevel.gameObject);
        LoadLevel();
        //UIManager.GetInstance().HideLose();
    }

    public void ClearCurrentLevel()
    {
        Destroy(currentLevel.gameObject);
    }

}
