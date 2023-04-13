using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text aliveText;

    private void Start()
    {
        GetInstance();
    }

    public void SetAliveText(int alive)
    {
        aliveText.text = "Alive : " + alive.ToString();
    }
}
