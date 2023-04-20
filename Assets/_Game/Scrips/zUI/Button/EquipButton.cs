using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ChoiceButton
{
    Pant, Head, Shield, Set
}
public class EquipButton : MonoBehaviour
{
    Equipment equipmentInfor;
    MeshRenderer meshRenderer;
    Button button;
    Text priceText;
    ChoiceButton choiceButton;
    SkinMenu Menu;
    public MeshRenderer MeshRenderer { get => meshRenderer; set => meshRenderer = value; }

    public Button Button
    {
        get
        {
            if (button == null)
            {
                button = GetComponent<Button>();
            }
            return button;
        }
    }

    public Equipment EquipmentInfor { get => equipmentInfor; set => equipmentInfor = value; }
    public Text PriceText { get => priceText; set => priceText = value; }
    internal ChoiceButton ChoiceButton { get => choiceButton; set => choiceButton = value; }

    public void Awake()
    {
        Button.onClick.AddListener(TaskOnClick);
    }
    public void TaskOnClick()
    {
        if (choiceButton is ChoiceButton.Pant)
        {
            GameManager.GetInstance().currentPlayer.SetPant(PoolingPro.GetInstance().pantMaterials[equipmentInfor.Id - 1]);
            if (SaveLoadManager.GetInstance().Data1.PantOwners.Contains(equipmentInfor.Id))
            {
                Menu.ButtonBuy.SetActive(false);
                Menu.ButtonEquip.SetActive(true);
            }
            else
            {
                Menu.ButtonBuy.SetActive(true);
                Menu.ButtonEquip.SetActive(false);
                SetPriceText(equipmentInfor.Price);
            }
        }
        if (choiceButton is ChoiceButton.Head)
        {
            GameManager.GetInstance().currentPlayer.SetHead(StaticData.HeadEnum[equipmentInfor.Name]);
            if (SaveLoadManager.GetInstance().Data1.HeadOwners.Contains(equipmentInfor.Name))
            {
                Menu.ButtonBuy.SetActive(false);
                Menu.ButtonEquip.SetActive(true);
            }
            else
            {
                Menu.ButtonBuy.SetActive(true);
                Menu.ButtonEquip.SetActive(false);
                SetPriceText(equipmentInfor.Price);
            }
        }

        Menu.CurrentEquipment = equipmentInfor;
    }
    public void SetPriceText(int price)
    {
        priceText.text = price.ToString();
    }
    public void SetSkinMenu(SkinMenu menu)
    {
        Menu = menu;
    }
}
