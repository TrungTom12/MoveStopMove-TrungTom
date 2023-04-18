using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    [System.Serializable]
    public class Data
    {

        int coin = 0;
        public string WeaponCurrent { get; set; }

        public string WeaponOwner { get; set; }
        public int Coin { get => coin; set => coin = value; }

        public int LevelID { get; set; }
    }

    [SerializeField] private string saveFileName = Constan.SAVE_FILE_NAME;
    [SerializeField] private bool loadOnStart = true;
    private Data data;
    private BinaryFormatter formatter;

    public Data Data1 { get => data; set => data = value; }

    private void Start()
    {
        formatter = new BinaryFormatter();

        if (loadOnStart)
        {
            Load();
        }
        UIManager.GetInstance().DisplayMainMenuPanel();
        Debug.Log(saveFileName);
    }

    public void Load()
    {

        try
        {
            FileStream file = new FileStream(saveFileName, FileMode.Open, FileAccess.Read);
            try
            {
                data = (Data)formatter.Deserialize(file);
                Debug.Log(data.Coin);
                Debug.Log(data.WeaponCurrent);
            }
            catch
            {
                Debug.Log("Cant Read Data");
                file.Close();
                Save();
            }
            file.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            Save();
        }
    }

    public void Save()
    {
        if (data == null)
        {
            data = new Data();
        }
        try
        {
            FileStream file = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
            formatter.Serialize(file, data);
            file.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
