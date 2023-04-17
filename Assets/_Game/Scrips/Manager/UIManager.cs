using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text aliveText;
    [SerializeField] private Text coinText;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject mainMenuPanel;
    private void Start()
    {
        GetInstance();
    }

    public void SetAliveText(int alive)
    {
        aliveText.text = "Alive : " + alive.ToString();
    }
    public void SetCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void UnActiveAllPanel()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        settingPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        playPanel.SetActive(false);
    }
    public void DisplayPlayPanel()
    {
        Time.timeScale = 1;
        UnActiveAllPanel();
        playPanel.SetActive(true);
    }

    public void DisplayWinPanel()
    {
        Time.timeScale = 0;
        UnActiveAllPanel();
        winPanel.SetActive(true);
    }
    public void DisplayLosePanel()
    {
        Time.timeScale = 0;
        UnActiveAllPanel();
        losePanel.SetActive(true);
    }
    public void DisplaySettingPanel()
    {
        Time.timeScale = 0;
        UnActiveAllPanel();
        settingPanel.SetActive(true);
    }
    public void DisplayMainMenuPanel()
    {
        UnActiveAllPanel();
        SetCoinText(SaveLoadManager.GetInstance().Data1.Coin);
        mainMenuPanel.SetActive(true);
    }

    //public void HideLose()
    //{
    //    losePanel.SetActive(false);
    //}
}
