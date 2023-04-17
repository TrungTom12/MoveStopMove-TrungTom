using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] private Image imageIcon;

    private void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log("Click");
        
    }

    public void SetUpData(IDataSkin _idata)
    {
        imageIcon.sprite = _idata.spriteIcon;
    }
}
