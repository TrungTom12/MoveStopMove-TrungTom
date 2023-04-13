using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RenItem : MonoBehaviour
{
    [SerializeField] DataSkin dataSkin;
    [SerializeField] private ButtonShop ItemPrefabs;
    
    void Start()
    {

        for (int i = 0; i < dataSkin.iDataSkin.Length; i++)
        {
            ButtonShop go =  Instantiate(ItemPrefabs,transform);
            go.SetUpData(dataSkin.iDataSkin[i]);
        }

    }

   
}
