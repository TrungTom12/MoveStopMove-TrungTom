using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;


public class SkinMenu : UICanvas
{
    [SerializeField] private Transform scrollView;
    [SerializeField] private Text coinText;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject equipButtonPrefabs;
    [SerializeField] List<Texture2D> headTextures;
    [SerializeField] List<Texture2D> pantTextures;
    [SerializeField] List<Texture2D> skinTextures;
    [SerializeField] List<Texture2D> armSkinTextures;
    List<EquipButton> equipButtons = new List<EquipButton>();
    [SerializeField] GameObject buttonBuy;
    [SerializeField] GameObject buttonEquip;
    ChoiceButton choiceButton;
    Equipment currentEquipment;

    public Equipment CurrentEquipment { get => currentEquipment; set => currentEquipment = value; }
    public GameObject ButtonBuy { get => buttonBuy; set => buttonBuy = value; }
    public GameObject ButtonEquip { get => buttonEquip; set => buttonEquip = value; }

    public override void Open()
    {
        base.Open();
        SetCoinText(SaveLoadManager.GetInstance().Data1.Coin);
        CameraFollow.GetInstance().ZoomIn();
        ButtonBuy.SetActive(false);
        ButtonEquip.SetActive(false);
    }
    public void HeadButton()
    {
        SetUpButton(headTextures, StaticData.headEquipments, ChoiceButton.Head);
        choiceButton = ChoiceButton.Head;
    }

    public void PantsButton()
    {
        SetUpButton(pantTextures, StaticData.pantEquipments, ChoiceButton.Pant);
        choiceButton = ChoiceButton.Pant;
    }

    public void ArmSkinButton()
    {
        SetUpButton(armSkinTextures, StaticData.shieldEquipments, ChoiceButton.Shield);
        choiceButton = ChoiceButton.Shield;
    }
    public void SkinButton()
    {
        SetUpButton(skinTextures, StaticData.setEquipments, ChoiceButton.Set);
        choiceButton = ChoiceButton.Set;
    }
    public void BuyButton()
    {
        int currentCoin = SaveLoadManager.GetInstance().Data1.Coin;
        int price = currentEquipment.Price;
        if (currentCoin >= price)
        {
            currentCoin -= price;
            SetCoinText(currentCoin);
            SaveLoadManager.GetInstance().Data1.Coin = currentCoin;
            if (choiceButton is ChoiceButton.Pant)
            {
                SaveLoadManager.GetInstance().Data1.PantOwners.Add(currentEquipment.Id);
            }
            if (choiceButton is ChoiceButton.Head)
            {
                Debug.Log(currentEquipment.Name);
                SaveLoadManager.GetInstance().Data1.HeadOwners.Add(currentEquipment.Name);
                Debug.Log(SaveLoadManager.GetInstance().Data1.HeadOwners[SaveLoadManager.GetInstance().Data1.HeadOwners.Count - 1]);
            }
            buttonBuy.SetActive(false);
            buttonEquip.SetActive(true);
            SaveLoadManager.GetInstance().Save();
        }
        else
        {
            return;
        }

    }
    public void EquipButton()
    {
        if (choiceButton is ChoiceButton.Pant)
        {
            SaveLoadManager.GetInstance().Data1.IdPantMaterialCurrent = currentEquipment.Id;

        }
        if (choiceButton is ChoiceButton.Head)
        {
            SaveLoadManager.GetInstance().Data1.HeadCurrent = currentEquipment.Name;
        }
        SaveLoadManager.GetInstance().Save();
    }

    public void ReturnButton()
    {
        ClearButton();
        NewUIManager.GetInstance().OpenUI<MainMenu>();
        Close(0);
    }
    public void SetCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void SetPriceText(int price)
    {
        priceText.text = price.ToString();
    }
    public void ClearButton()
    {
        while (equipButtons.Count != 0)
        {
            Destroy(equipButtons[0].gameObject);
            equipButtons.RemoveAt(0);

        }
    }
    public void SetUpButton(List<Texture2D> textures, List<Equipment> equipments, ChoiceButton choiceButton)
    {
        ClearButton();
        for (int i = 0; i < textures.Count; i++)
        {
            equipButtonPrefabs.GetComponent<RawImage>().texture = textures[i];
            EquipButton button = Instantiate(equipButtonPrefabs, scrollView).GetComponent<EquipButton>();
            button.SetSkinMenu(this);
            equipButtons.Add(button);
            equipButtons[i].EquipmentInfor = equipments[i];
            equipButtons[i].PriceText = priceText;
            equipButtons[i].ChoiceButton = choiceButton;
        }
    }
    public override void Close(float delayTime)
    {
        GameManager.GetInstance().currentPlayer.OnInit();
        CameraFollow.GetInstance().ZoomOut();
        base.Close(delayTime);
    }
}
