using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasShop : MonoBehaviour
{
    public DataSkin skin;
    public HatItem hatItemPrefab;
    public Transform container;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < skin.hatDatas.Length; i++)
        {
            Instantiate(hatItemPrefab, container).SetData(skin.hatDatas[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
