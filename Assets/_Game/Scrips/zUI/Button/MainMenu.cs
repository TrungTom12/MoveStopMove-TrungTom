using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UICanvas
{
    [SerializeField] private Text coinText;
    public override void Open()
    {
        base.Open();
        SetCoinText(SaveLoadManager.GetInstance().Data1.Coin);
        if (GameManager.GetInstance().currentPlayer == null || !GameManager.GetInstance().currentPlayer.gameObject.activeSelf)
            GameManager.GetInstance().currentPlayer = PoolingPro.GetInstance().GetFromPool(CharacterType.Player.ToString(), Vector3.zero).GetComponent<Player>();
    }
    public Text CoinText { get => coinText; set => coinText = value; }

    public void WeaponButton()
    {
        NewUIManager.GetInstance().OpenUI<WeaponMenu>();
        Close(0);
    }
    public void PlayButton()
    {
        NewUIManager.GetInstance().OpenUI<PlayingMenu>();
        LevelManager.GetInstance().LoadLevel();
        Close(0);
    }
    public void SettingButton()
    {

        NewUIManager.GetInstance().OpenUI<SettingMenu>();
        Close(0);
    }
    public void SkinButton()
    {
        NewUIManager.GetInstance().OpenUI<SkinMenu>();
        Close(0);
    }
    public void SetCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }
    public override void Close(float delayTime)
    {
        base.Close(delayTime);

    }

    public void MusicButton()
    {
        SoundManager2.GetInstance().SwitchSoundBackGround();
    }
}
