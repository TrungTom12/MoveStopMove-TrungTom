using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


[CreateAssetMenu(fileName = "DataSkin", menuName = "ScriptableObject/DataSkin",order = 1)]

public class DataSkin : ScriptableObject
{
    public int id;
    public IDataSkin[] iDataSkin;
}