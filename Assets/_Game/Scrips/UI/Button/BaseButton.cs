using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    [Header("Base Button")]
    [SerializeField] protected Button button;

    protected void Start()
    {
        
    }

    protected virtual void LoadButton()
    {
        if (this.button != null) return;
        {
            this.button = GetComponent<Button>();

        }
    }

    protected virtual void AddOnClickEvent()
    {
        //this.button.OnClick.AddListener(this.OnClick);
    }

}
