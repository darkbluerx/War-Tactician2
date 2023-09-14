using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
//using UnityEngine.UIElements;

public class Kauppa : MonoBehaviour
{
    public static Kauppa Instance { get; private set; }

    public int player1Money;
    public TMP_Text player1MoneyText;
    [Space]

    [Header("ButtonInffoSO ScritableObjects")]
    public ButtonInffoSO[] buttonInffos;
    [Space]

    [Header("Turn on/off Button GameObjects")]
    public GameObject[] buttonGameobjects;
    [Space]

    [Header("Unit Buttons")]
    public UnitButton[] unitButtons;
    public Button[] myPurchaseButtons;
    [Space]

    [Header("GamePlay Buttons")]
    public Button[] gamePlayButtons; //size 5,
    public GameObject[] gamePlayButtonsGameObjects; //size 5,

    public UnityEvent unitButtonEvent;

 
    private void Awake()
    {
        if (Instance != null) //Singleton pattern
        {
            Debug.LogError("There's more than one Kauppa! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        //Show the buttons gameobjects
        for (int i = 0; i < buttonInffos.Length; i++)
        {
            buttonGameobjects[i].SetActive(true);
        }

        UpdateMoneyText();
        LoadUnitInffos();
        //CheckPurchaseable();

        //Purchase button presses
        for (int i = 0; i < myPurchaseButtons.Length; i++)
        {
            int buttonIndex = i; // Capture the index in a local variable
            myPurchaseButtons[i].onClick.RemoveAllListeners();
            myPurchaseButtons[i].onClick.AddListener(() => PurchaseUnit(buttonIndex));
        } 
    }

    public void AddMoney()
    {
        player1Money++;
        UpdateMoneyText();
        //CheckPurchaseable(buttonIndex);
    }

    public void CheckPurchaseable(int buttonIndex)
    {
        for (int i = 0; i < buttonInffos.Length; i++)
        {
            if (player1Money >= buttonInffos[i].unitCost)
            {
                myPurchaseButtons[i].interactable = true;

                //myPurchaseButtons[buttonIndex].interactable = false;
                unitButtonEvent.Invoke();
            }
            else
            {
                myPurchaseButtons[i].interactable = false;
            }
        }
    }

    private void UpdateMoneyText()
    {
        player1MoneyText.text = "Gold:" + player1Money.ToString();
    }

    public void PurchaseUnit(int buttonIndex)
    {
        if (player1Money >= buttonInffos[buttonIndex].unitCost)
        {
            player1Money -= buttonInffos[buttonIndex].unitCost;

            UpdateMoneyText();
            CheckPurchaseable(buttonIndex);
            UnlockUnit(buttonIndex);
        }
    }


    //etsi json harjoitus projekteista ja katso läpi. katso vielä youtubesta tai kysy Chat Gtp:ltä miten tämä tehdään
    //jos ostat yksikön, sen buttinIndeksi tallennetaan tai yksikkö vastaava string "Knight"

    //tämän perusteella  unit prefabbia kutsutaan ja se instansoidaan/ asetetaan Gameplay Sceneen canvasiin
    //GamePlay scenessä on pelaajaa kohti 5 nappia, eka nappi vastaa Footmen jne. Tämän tarkoitus on yksinkertaistaa toimintaa?
    //tämän jälkeen UnitManageri kutsuu unitin joka on nappulassa eventillä?, tämä toiminto ei välitä mikä unit on nappulassa, vaan kutsuu sen mikä siinä on


    public void UnlockUnit(int buttonIndex)
    {
        //buttonGameobjects[buttonIndex].SetActive(false);

        int gamePlayButtonIndex = buttonIndex / 3; // Calculate the gamePlayButtons index

        if (gamePlayButtonIndex >= 0 && gamePlayButtonIndex < gamePlayButtons.Length)
        {
            //gamePlayButtons[gamePlayButtonIndex].interactable = true;
            gamePlayButtonsGameObjects[gamePlayButtonIndex].SetActive(true);    
        }
    }

    public void LoadUnitInffos() //Load the unit info to the buttons: images, names and costs
    {
        for (int i = 0; i < buttonInffos.Length; i++)
        {
            unitButtons[i].unitNameText.text = buttonInffos[i].unitName;
            unitButtons[i].unitCostText.text = buttonInffos[i].unitCost.ToString();
            unitButtons[i].backgroundImage.sprite = buttonInffos[i].backgroundPicture;
            unitButtons[i].unitImage.sprite = buttonInffos[i].unitPicture;
        }
    }
}
