using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenu : UICanvas
{
    [SerializeField] private Text coinText;
    private WeaponType weaponTypeShowing;
    private GameObject weaponShow;
    private List<WeaponType> weaponTypes = new List<WeaponType>();
    [SerializeField] private Text weaponName;
    [SerializeField] private Text weaponPrice;
    [SerializeField] private Transform weaponShowPos;
    private int page = 0;
    private Camera cameraShowWeapon;
    protected override void OnInit()
    {
        base.OnInit();
        weaponTypes.Add(WeaponType.Knife);
        weaponTypes.Add(WeaponType.Boomerang);
        weaponTypes.Add(WeaponType.Axe);
        weaponTypes.Add(WeaponType.Candy);
        SetPageInformation(page);


    }
    public void SetPageInformation(int page)
    {
        if (weaponShow != null)
        {
            PoolingPro.GetInstance().ReturnToPool(PoolingPro.GetInstance().weaponShows[weaponTypeShowing].ToString(), weaponShow);
        }
        weaponTypeShowing = weaponTypes[page];
        weaponName.text = weaponTypeShowing.ToString();
        weaponShow = PoolingPro.GetInstance().GetFromPool(PoolingPro.GetInstance().weaponShows[weaponTypeShowing].ToString(), weaponShowPos.position);
        weaponShow.transform.SetParent(weaponShowPos);
        weaponShow.transform.localScale = Vector3.one;
        SetWeaponPrice(StaticData.PriceWeapon[weaponTypeShowing]);
    }
    public void SetWeaponPrice(float price)
    {
        weaponPrice.text = price.ToString();
    }
    public override void Open()
    {
        base.Open();
        SetCoinText(SaveLoadManager.GetInstance().Data1.Coin);
    }
    public void HomeButton()
    {
        NewUIManager.GetInstance().OpenUI<MainMenu>();
        Close(0);
    }
    public void EquipButton()
    {
        if (SaveLoadManager.GetInstance().Data1.WeaponOwners == null)
        {
            SaveLoadManager.GetInstance().Data1.WeaponOwners = new List<WeaponType>();
            Debug.Log("Owner null");
        }
        if (SaveLoadManager.GetInstance().Data1.WeaponOwners.Contains(weaponTypeShowing))
        {
            SaveLoadManager.GetInstance().Data1.WeaponCurrent = weaponTypeShowing.ToString();
            Debug.Log("Equip " + weaponTypeShowing.ToString());
            GameManager.GetInstance().currentPlayer.OnInit();
            SaveLoadManager.GetInstance().Save();
        }
        else
        {
            Debug.Log("Not Own This Equipment");
        }

    }
    public void BuyButton()
    {
        if (SaveLoadManager.GetInstance().Data1.WeaponOwners == null)
        {
            SaveLoadManager.GetInstance().Data1.WeaponOwners = new List<WeaponType>();
            Debug.Log("Owner null");
        }
        if (SaveLoadManager.GetInstance().Data1.WeaponOwners.Contains(weaponTypeShowing))
        {
            Debug.Log("You bought it");
            return;
        }
        if (StaticData.PriceWeapon[weaponTypeShowing] <= SaveLoadManager.GetInstance().Data1.Coin)
        {
            SaveLoadManager.GetInstance().Data1.Coin -= StaticData.PriceWeapon[weaponTypeShowing];
            SaveLoadManager.GetInstance().Data1.WeaponOwners.Add(weaponTypeShowing);
            SaveLoadManager.GetInstance().Save();
            Debug.Log("Success Buy");
            SetCoinText(SaveLoadManager.GetInstance().Data1.Coin);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    public void NextButton()
    {
        if (page >= weaponTypes.Count - 1)
        {
            return;
        }
        page++;
        SetPageInformation(page);
    }
    public void PrevButton()
    {
        if (page <= 0)
        {
            return;
        }
        page--;
        SetPageInformation(page);
    }
    public void SetCoinText(int coin)
    {
        coinText.text = "Coin : " + coin.ToString();
    }
}
