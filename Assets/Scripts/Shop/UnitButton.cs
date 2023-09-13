using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UnitButton : MonoBehaviour
{
    public TMP_Text unitNameText;
    public Image buttonImage;

    public TMP_Text unitCostText;

    [SerializeField] Button button;
    [SerializeField] Image selectedImage;

    public UnityEvent buttonEvent;

    private void Awake()
    {
        button = GetComponent<Button>(); //get reference to the button component
        buttonEvent.AddListener(ClickButton);
    }

    public void OnEnable()
    {
        button.onClick.RemoveAllListeners();
        //button.onClick.AddListener(ShowSelectedButton);
        button.onClick.AddListener(ClickButton);
        
        //button.onClick.AddListener(ClickButton);
        //Kauppa.Instance.unitButtonEvent.AddListener(ShowSelectedButton);
        //toimii
        
    }

    public void ClickButton()
    {
        buttonEvent.Invoke();
        selectedImage.enabled = true;
    }
}


