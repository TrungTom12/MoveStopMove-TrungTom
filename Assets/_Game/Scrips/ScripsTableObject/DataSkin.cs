using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


[CreateAssetMenu(fileName = "DataSkin", menuName = "ScriptableObject/DataSkin",order = 1)]
//public class DataSkin : ScriptableObject
//{
//    public HatData[] hatDatas;
//}

//[System.Serializable]
//public class HatData
//{
//    public int idSkin;
//    public string name;
//    public Sprite icon;
//}

public class DataSkin : ScriptableObject
{
    [SerializeField] private string nameData;
    public int id;
    public IDataSkin[] iDataSkin;
}