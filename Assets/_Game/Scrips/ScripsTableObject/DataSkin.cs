using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DataSkin", menuName = "ScriptableObject/DataSkin")]
public class DataSkin : ScriptableObject
{
    public HatData[] hatDatas;
}

[System.Serializable]
public class HatData
{
    public int idSkin;
    public string name;
    public Sprite icon;
}
