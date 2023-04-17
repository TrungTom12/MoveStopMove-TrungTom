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
    
    public List<Button> button = new List<Button>();
   // private List
    void Start()
    {
        for (int i = 0; i < button.Count; i++)
        {
            int index = i;
            button[index].onClick.AddListener(() => 
            { GetData(index); });
            Debug.Log("Da ckick dc ");
        }
        GetData(0);
    }

    public void GetData(int index)
    {
        for (int i = 0; i < dataSkin.iDataSkin.Length; i++)
        {
            ButtonShop go = Instantiate(ItemPrefabs, transform);
            go.SetUpData(dataSkin.iDataSkin[i]);
        }
    }
   
}
