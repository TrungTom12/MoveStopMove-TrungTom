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

        public Data()
        {
            Coin = 0;
            WeaponCurrent = "Axe";
            IdPantMaterialCurrent = 1;
            HeadCurrent = "Head1";
            WeaponOwners = new List<WeaponType>();
            PantOwners = new List<int>();
            HeadOwners = new List<string>();
            EquipOwners = new List<Equipment>();
            LevelID = 1;
        }
        public int IdPantMaterialCurrent { get; set; }
        public string WeaponCurrent { get; set; }

        public string HeadCurrent { get; set; }
        public List<WeaponType> WeaponOwners { get; set; }
        public List<int> PantOwners { get; set; }
        public List<string> HeadOwners { get; set; }
        public List<Equipment> EquipOwners;
        public int Coin { get; set; }

        public int LevelID { get; set; }
    }

    [SerializeField] private string saveFileName = Constan.SAVE_FILE_NAME;
    [SerializeField] private bool loadOnStart = true;
    private Data data;
    private BinaryFormatter formatter;

    public Data Data1 { get => data; set => data = value; }

    public void OnInit()
    {
        formatter = new BinaryFormatter();

        if (loadOnStart)
        {
            Load();
        }
        //UIManager.GetInstance().DisplayMainMenuPanel();
        Debug.Log(saveFileName);
        foreach (string x in data.HeadOwners)
        {
            Debug.Log(x);
        }
    }

    public void Load()
    {

        try
        {
            FileStream file = new FileStream(saveFileName, FileMode.Open, FileAccess.Read);
            try
            {
                data = (Data)formatter.Deserialize(file);
                if (data.Coin < 300)
                {
                    data.Coin = 999;
                }
                if (data.WeaponCurrent == "")
                {
                    data.WeaponCurrent = "Axe";
                }
                if (data.IdPantMaterialCurrent <= 0)
                {
                    data.IdPantMaterialCurrent = 0;
                }
                if (data.PantOwners == null)
                {
                    data.PantOwners = new List<int>();
                }
                if (data.HeadOwners == null)
                {
                    data.HeadOwners = new List<string>();
                }
                if (data.EquipOwners == null)
                {
                    data.EquipOwners = new List<Equipment>();
                }
                if (data.HeadCurrent == null)
                {
                    data.HeadCurrent = "Head1";
                }
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
